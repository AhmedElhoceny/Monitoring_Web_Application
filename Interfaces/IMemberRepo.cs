using Monitoring_First_Version.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitoring_First_Version.Interfaces
{
    interface IMemberRepo : IGeneral<Member>
    {
        bool ConfirmUserAcceptance(string UserName);
        int LogInConfirmation(string UserName, string Password);
        List<Tasks> GetAllMemberTasks(int id);
        List<Team> GetAllMemberTeams(int id);
        void TaskDone(int MemberID, int TaskID);
        void FailedTask(int MemberID, int TaskID);
        void TaskDoneLate(int MemberID, int TaskID);
        void AddTaskToMember(int MemberID, Tasks NewTask);
        void AddTeamToMember(int MemberID, Team NewTeam);
        void RemoveTaskFromMember(int MemberID, Tasks NewTask);
        void RemoveTeamFromMember(int MemberID, Team NewTeam);
    }
}
