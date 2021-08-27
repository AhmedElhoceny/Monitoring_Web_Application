using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monitoring_First_Version.Models
{
    public class Tasks
    {
        public int id { get; set; }
        public string taskName { get; set; }
        public List<Member> taskMembers { get; set; }
        public string taskState { get; set; }
        public DateTime taskDeadLine { get; set; }
    }
}