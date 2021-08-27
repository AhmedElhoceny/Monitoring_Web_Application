using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monitoring_First_Version.Models
{
    public class Member
    {
        public int id { get; set; }
        public string memberName { get; set; }
        public string memberEmail { get; set; }
        public string memberPassword { get; set; }
        public string memberPhoneNumber { get; set; }
        public bool Accepted { get; set; }
        public ICollection<Team> memberJoinedTeaams { get; set; }
        public ICollection<Tasks> memberTasks { get; set; }
        public int memberRank { get; set; }
    }
    public enum TeamList
    {
        WebDevelopment , IOT , MachineLearning , EmbeddedSystems
    }
}