using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Monitoring_First_Version.Models
{
    public class ContextClass : DbContext
    {
        public ContextClass()
        {

        }
        public DbSet<Member> Members { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Member>()
                        .HasMany<Team>(s => s.memberJoinedTeaams)
                        .WithMany(c => c.teamMembers)
                        .Map(cs =>
                        {
                            cs.MapLeftKey("MemberRefId");
                            cs.MapRightKey("TeamRefId");
                            cs.ToTable("MemberTeam");
                        });
            modelBuilder.Entity<Member>()
                         .HasMany<Tasks>(s => s.memberTasks)
                         .WithMany(c => c.taskMembers)
                         .Map(cs =>
                         {
                             cs.MapLeftKey("MemberRefId");
                             cs.MapRightKey("taskRefId");
                             cs.ToTable("Membertask");
                         });
        }
    }
}