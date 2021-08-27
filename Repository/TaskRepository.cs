using Monitoring_First_Version.Interfaces;
using Monitoring_First_Version.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Monitoring_First_Version.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private ContextClass MyDB { get; }

        public TaskRepository()
        {
            this.MyDB = new ContextClass();
        }

        public void AddItem(Tasks Item)
        {
            MyDB.Tasks.Add(Item);
            MyDB.SaveChanges();
        }

        public void AddMemberToTask(int TaskID, Member AddedMember)
        {
            FindDataByID(TaskID).taskMembers.Add(AddedMember);
            MyDB.SaveChanges();
        }

        public void DeleteItem(int id)
        {
            MyDB.Tasks.Remove(FindDataByID(id));
            MyDB.SaveChanges();
        }

        public Tasks FindDataByID(int id)
        {
            return MyDB.Tasks.Where(x => x.id == id).Include(x => x.taskMembers).SingleOrDefault();
        }

        public Tasks FindDataByName(string Name)
        {
            return MyDB.Tasks.Where(x => x.taskName == Name).Include(x => x.taskMembers).SingleOrDefault();
        }

        public List<Tasks> GetAllData()
        {
            return MyDB.Tasks.ToList();
        }

        public List<Member> GetAllTaskMembers(int TaskID)
        {
            return FindDataByID(TaskID).taskMembers;
        }

        public void UpdateItem(Tasks UpdatedData)
        {
            var SearchedData = FindDataByID(UpdatedData.id);
            SearchedData.taskDeadLine = UpdatedData.taskDeadLine;
            SearchedData.taskName = UpdatedData.taskName;
            SearchedData.taskState = UpdatedData.taskState;
            MyDB.SaveChanges();
        }
    }
}