using Monitoring_First_Version.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitoring_First_Version.Interfaces
{
    interface ITeamRepository : IGeneral<Team>
    {
        void AddMemberToTeam(int TeamID, int MemberID);
        List<Member> GetAllTeamMembers(int TeamID);
    }
}
