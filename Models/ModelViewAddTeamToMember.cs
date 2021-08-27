using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monitoring_First_Version.Models
{
    public class ModelViewAddTeamToMember
    {
        public IEnumerable<Team> Teams { get; set; }
        public Member OurMmeber { get; set; }
    }
}