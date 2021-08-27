using Monitoring_First_Version.Interfaces;
using Monitoring_First_Version.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Monitoring_First_Version.Repository
{
    public class TeamRepository : ITeamRepository
    {
        private readonly MemberRepository memberRepo;

        private ContextClass MyDB { get; }

        public TeamRepository()
        {
            this.MyDB = new ContextClass();
            memberRepo = new MemberRepository();
        }


        public void AddItem(Team Item)
        {
            MyDB.Teams.Add(Item);
            MyDB.SaveChanges();
        }

        public void AddMemberToTeam(int TeamID, int MemberID)
        {
            FindDataByID(TeamID).teamMembers.Add(memberRepo.FindDataByID(MemberID));
            MyDB.SaveChanges();
        }

        public void DeleteItem(int id)
        {
            MyDB.Teams.Remove(FindDataByID(id));
            MyDB.SaveChanges();
        }

        public Team FindDataByID(int id)
        {
            return MyDB.Teams.Where(x => x.id == id).Include(x => x.teamMembers).SingleOrDefault();
        }

        public Team FindDataByName(string Name)
        {
            return MyDB.Teams.Where(x => x.TeamName == Name).Include(x => x.teamMembers).FirstOrDefault();
        }

        public List<Team> GetAllData()
        {
            return MyDB.Teams.Include(x => x.teamMembers).ToList();
        }

        public List<Member> GetAllTeamMembers(int TeamID)
        {
            return FindDataByID(TeamID).teamMembers.ToList();
        }

        public void UpdateItem(Team UpdatedData)
        {
            var SearchedData = FindDataByID(UpdatedData.id);
            SearchedData.TeamName = UpdatedData.TeamName;
            MyDB.SaveChanges();
        }
    }
}