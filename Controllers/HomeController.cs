using Monitoring_First_Version.Models;
using Monitoring_First_Version.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Monitoring_First_Version.Controllers
{
    public class HomeController : Controller
    {
        private  MemberRepository memberRepository;
        private TeamRepository TeamRepository;
        private TaskRepository TaskRepository;
        public HomeController()
        {
            this.memberRepository = new MemberRepository();
            this.TeamRepository = new TeamRepository();
            this.TaskRepository = new TaskRepository();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }
        public ActionResult SignUpPost(Member NewMember)
        {
            memberRepository.AddItem(NewMember);
            return RedirectToAction(nameof(LogIn));
        }
        public ActionResult LogIn()
        {
            return View();
        }
        public ActionResult LogInPost(Member NewMember)
        {
            int result = 0;
            if (memberRepository.ConfirmUserAcceptance(NewMember.memberName))
            {
                result = memberRepository.LogInConfirmation(NewMember.memberName, NewMember.memberPassword);
            }
            if(result != 0)
            {
                if (result == 2)
                {
                    return RedirectToAction(nameof(AdminSide));
                }
                return RedirectToAction(nameof(Profile) , new { id = result });
            }
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Profile(int id)
        {
            var searchedProfile = memberRepository.FindDataByID(id);
            var ModelData = new Member() { Accepted = searchedProfile.Accepted, id = searchedProfile.id, memberEmail = searchedProfile.memberEmail, memberJoinedTeaams = searchedProfile.memberJoinedTeaams, memberName = searchedProfile.memberName, memberPassword = searchedProfile.memberPassword, memberPhoneNumber = searchedProfile.memberPhoneNumber, memberRank = searchedProfile.memberRank, memberTasks = searchedProfile.memberTasks.Where(x => x.taskState == "Current").ToList()};
            if(searchedProfile.memberTasks.Count() != 0)
            {
                ViewBag.TasksCurrent = searchedProfile.memberTasks.Where(x => x.taskState == "Current").Count();
                ViewBag.TasksDone = searchedProfile.memberTasks.Where(x => x.taskState == "Done").Count();
                ViewBag.TasksLate = searchedProfile.memberTasks.Where(x => x.taskState == "Late").Count();
                ViewBag.TasksFailed = searchedProfile.memberTasks.Where(x => x.taskState == "Failed").Count();
            }
            else
            {
                ViewBag.TasksCurrent = 0;
                ViewBag.TasksDone = 0;
                ViewBag.TasksLate = 0;
                ViewBag.TasksFailed = 0;
            }
            return View(ModelData);
        }
        public ActionResult AdminSide()
        {
            var members = memberRepository.GetAllData();
            return View(members);
        }
        public string ActivateMember(int id)
        {
            try
            {
                var Member = memberRepository.FindDataByID(id);
                Member.Accepted = true;
                memberRepository.UpdateItem(Member);
                return "Success";
            }
            catch (Exception)
            {
                return "Failed";
            }

        }

        public ActionResult EditMmeber(int id)
        {
            var member = memberRepository.FindDataByID(id);
            return View("EditMmeber", member);
        }
        public ActionResult EditMmeberPost(Member EditedData)
        {
            memberRepository.UpdateItem(EditedData);
            return RedirectToAction(nameof(AdminSide));
        }
        public ActionResult AddTeamToMember(int id)
        {
            var member = memberRepository.FindDataByID(id);
            var Teams = TeamRepository.GetAllData();
            ModelViewAddTeamToMember ResultViewData = new ModelViewAddTeamToMember() { OurMmeber = member, Teams = Teams };
            return View(ResultViewData);
        }
        public ActionResult AddTeamPost(int id , string TeamName)
        {
            var SearchedMember = memberRepository.FindDataByID(id);
            var AddedTeam = TeamRepository.FindDataByName(TeamName);
            if(SearchedMember.memberJoinedTeaams.Where(x => x.TeamName == TeamName).Count() == 1)
            {
                return Json(new JsonClass() { Result = "No" }, JsonRequestBehavior.AllowGet);
            }
            SearchedMember.memberJoinedTeaams.Add(AddedTeam);
            memberRepository.UpdateItem(SearchedMember);
            return Json(new JsonClass() { Result = "OK" }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult AddTaskToMember(int id)
        {
            var member = memberRepository.FindDataByID(id);
            var OurData = new ModelViewAddTaskToMember() { OurMember = member, Task = new Tasks() };
            return View(OurData);
        }
        public ActionResult AddTaskToMemberPost(ModelViewAddTaskToMember ReturnData)
        {
            var NewTask = new Tasks() { taskDeadLine = ReturnData.Task.taskDeadLine, taskMembers = new List<Member>() { memberRepository.FindDataByID(ReturnData.OurMember.id) }, taskName = ReturnData.Task.taskName, taskState = "Current" };
            memberRepository.AddTaskToMember(ReturnData.OurMember.id, NewTask);
            return RedirectToAction(nameof(AdminSide));
        }
        public ActionResult DeleteMember(int id)
        {
            memberRepository.DeleteItem(id);
            return RedirectToAction(nameof(AdminSide));
        }
        public ActionResult DoneTask(int MemberID, int TaskID )
        {
            try
            {
                var DateTimeSend = DateTime.Now;
                var ResultDate = DateTime.Compare(DateTimeSend, TaskRepository.FindDataByID(TaskID).taskDeadLine);
                if (ResultDate < 0)
                {
                    memberRepository.TaskDone(MemberID, TaskID);
                }
                else
                {
                    memberRepository.TaskDoneLate(MemberID, TaskID);
                }
                return Json(new JsonClass() { Result = "OK" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new JsonClass() { Result = "No" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FailTask(int MemberID , int TaskID)
        {
            try
            {
                memberRepository.FailedTask(MemberID, TaskID);
                return Json(new JsonClass() { Result = "OK" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new JsonClass() { Result = "No" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}