using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monitoring_First_Version.Models
{
    public class Team
    {
        public int id { get; set; }
        public string TeamName { get; set; }
        public List<Member> teamMembers { get; set; }
    }
}