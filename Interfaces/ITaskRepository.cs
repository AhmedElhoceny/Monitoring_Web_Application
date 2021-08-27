using Monitoring_First_Version.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitoring_First_Version.Interfaces
{
    interface ITaskRepository : IGeneral<Tasks>
    {
        void AddMemberToTask(int TaskID, Member AddedMember);
        List<Member> GetAllTaskMembers(int TaskID);
    }
}
