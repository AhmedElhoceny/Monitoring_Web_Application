using Monitoring_First_Version.Interfaces;
using Monitoring_First_Version.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Monitoring_First_Version.Repository
{
    public class MemberRepository : IMemberRepo
    {
        public ContextClass myDB { get; set; }
        public MemberRepository()
        {
            this.myDB = new ContextClass();
        }


        public void AddItem(Member Item)
        {
            var flag = 0;
            foreach (var Member in GetAllData())
            {
                if(Member.memberName == Item.memberName || Member.memberPassword == Item.memberPassword)
                {
                    flag = 1;
                }
            }
            if(flag == 0)
            {
                Item.Accepted = false;
                myDB.Members.Add(Item);
                myDB.SaveChanges();
            }
        }

        public void AddTaskToMember(int MemberID, Tasks NewTask)
        {
            FindDataByID(MemberID).memberTasks.Add(NewTask);
            myDB.SaveChanges();
        }

        public void AddTeamToMember(int MemberID, Team NewTeam)
        {
            FindDataByID(MemberID).memberJoinedTeaams.Add(NewTeam);
            myDB.SaveChanges();

        }

        public bool ConfirmUserAcceptance(string UserName)
        {
            return FindDataByName(UserName).Accepted;
        }

        public void DeleteItem(int id)
        {
            myDB.Members.Remove(FindDataByID(id));
            myDB.SaveChanges();
        }

        public void FailedTask(int MemberID, int TaskID)
        {
            FindDataByID(MemberID).memberTasks.Where(x => x.id == TaskID).SingleOrDefault().taskState = "Failed";
            myDB.SaveChanges();
        }

        public Member FindDataByID(int id)
        {
            return myDB.Members.Where(x => x.id == id).Include(x => x.memberJoinedTeaams).Include(X => X.memberTasks).SingleOrDefault();
        }

        public Member FindDataByName(string Name)
        {
            return myDB.Members.Where(x => x.memberName == Name).Include(x => x.memberJoinedTeaams).Include(X => X.memberTasks).SingleOrDefault();
        }

        public List<Member> GetAllData()
        {
            return myDB.Members.Include(x => x.memberJoinedTeaams).Include(x => x.memberTasks).ToList();
        }

        public List<Tasks> GetAllMemberTasks(int id)
        {
            return FindDataByID(id).memberTasks.ToList();
        }

        public List<Team> GetAllMemberTeams(int id)
        {
            return FindDataByID(id).memberJoinedTeaams.ToList();
        }

        public int LogInConfirmation(string UserName, string Password)
        {
            var Result = myDB.Members.Where(x => x.memberName == UserName && x.memberPassword == Password).SingleOrDefault();
            if(Result != null)
            {
                return Result.id;
            }
            return 0;
        }

        public void RemoveTaskFromMember(int MemberID, Tasks NewTask)
        {
            FindDataByID(MemberID).memberTasks.Remove(NewTask);
            myDB.SaveChanges();
        }

        public void RemoveTeamFromMember(int MemberID, Team NewTeam)
        {
            FindDataByID(MemberID).memberJoinedTeaams.Remove(NewTeam);
            myDB.SaveChanges();
        }

        public void TaskDone(int MemberID, int TaskID)
        {
            FindDataByID(MemberID).memberTasks.Where(x => x.id == TaskID).SingleOrDefault().taskState = "Done";
            myDB.SaveChanges();
        }

        public void TaskDoneLate(int MemberID, int TaskID)
        {
            FindDataByID(MemberID).memberTasks.Where(x => x.id == TaskID).SingleOrDefault().taskState = "Late";
            myDB.SaveChanges();
        }

        public void UpdateItem(Member UpdatedData)
        {
            var searchedData = FindDataByID(UpdatedData.id);
            searchedData.memberEmail = UpdatedData.memberEmail;
            searchedData.memberName = UpdatedData.memberName;
            searchedData.memberPassword = UpdatedData.memberPassword;
            searchedData.memberPhoneNumber = UpdatedData.memberPhoneNumber;
            searchedData.memberRank = UpdatedData.memberRank;
            myDB.SaveChanges();
        }
    }
}