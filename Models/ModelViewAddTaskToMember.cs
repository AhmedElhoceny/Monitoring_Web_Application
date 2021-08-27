using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monitoring_First_Version.Models
{
    public class ModelViewAddTaskToMember
    {
        public Member OurMember { get; set; }
        public Tasks Task { get; set; }
    }
}