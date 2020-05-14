using Microsoft.AspNet.Identity;
using PlatformaManagementActivitati.Models;
using PlatformaManagementActivitati.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlatformaManagementActivitati.Controllers
{
    public class TeamController : Controller
    {
        private ApplicationDbContext _context;
        public TeamController ()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose (bool disposing)
        {
            _context.Dispose();
        }
        [Authorize(Roles = "User,Membru,Organizator,Administrator")]
        public ActionResult AddTeam (int id)
        {
            var team = new Team
            {
                ProjectId = id
            };
            var viewModel = new AddTeamViewModel
            {
                Team = team
            };
            return View("AddTeam", viewModel);
        }
        [Authorize(Roles = "User,Membru,Organizator,Administrator")]
        public ActionResult Edit (int id)
        {
            var viewModel = _context.Teams.SingleOrDefault(c => c.Id == id);
            if (viewModel == null)
                return HttpNotFound();
            return View("Edit", viewModel);
        }
        [Authorize(Roles = "User,Membru,Organizator,Administrator")]
        [HttpPost]
        public ActionResult SaveNewTeam (Team team)
        {
            var viewModel1 = new AddTeamViewModel
            {
                Team = team
            };
            if (!ModelState.IsValid)
                return View("AddTeam", viewModel1);

            if(team.Id == 0)
            {
                _context.Teams.Add(team);
                _context.SaveChanges();
                TempData["message"] = "Echipa a fost adaugat!";
                return RedirectToAction("Show", "Project", new { id = team.ProjectId });
            }
            var dbTeam = _context.Teams.SingleOrDefault(c => c.Id == team.Id);
            if (dbTeam == null)
                return HttpNotFound();
            dbTeam.Name = team.Name;
            _context.SaveChanges();
            TempData["message"] = "Echipa a fost modificata!";
            return RedirectToAction("ShowTeam", new { id = team.Id });
        }
        [Authorize(Roles = "User,Membru,Organizator,Administrator")]
        public ActionResult ShowTeam (int id)
        {
            var list = _context.MemberToTeams.Where(c => c.TeamId == id).ToList();
            ICollection<String> membersName = new List<String>();
            ICollection<ApplicationUser> posibleNewMembers = _context.Users.ToList();
            foreach (var item in list)
            {
                var user = _context.Users.Single(c => c.Id == item.MemberId);
                posibleNewMembers.Remove(user);
                var userName = user.UserName;
                membersName.Add(userName);
            }

            var viewModel = new ShowTeamViewModel
            {
                Team = _context.Teams.Single(c => c.Id == id),
                Members = membersName,
                PosibleNewMembers = posibleNewMembers,
                NewMemberToTeam = new MemberToTeam
                {
                    TeamId = id
                }
            };
            viewModel.Assignments = _context.Assignments.Where(c => c.TeamId == id);
            viewModel.UtilizatorCurent = User.Identity.GetUserId();
            viewModel.EsteAdmin = User.IsInRole("Administrator");
            viewModel.AfisareButoane = false;
            viewModel.AfisareButoane_2 = false;
            var currentUserId = User.Identity.GetUserId();
            var userId = _context.Projects.SingleOrDefault(c => c.Id == viewModel.Team.ProjectId).UserId;
            var memberToTeam = _context.MemberToTeams.SingleOrDefault(c => c.MemberId == currentUserId && c.TeamId == id);

            if (memberToTeam != null || userId == currentUserId)
                viewModel.AfisareButoane_2 = true;
            if (userId == currentUserId || User.IsInRole("Administrator"))
                viewModel.AfisareButoane = true;

            return View("ShowTeam", viewModel);
        }
        [Authorize(Roles = "User,Membru,Organizator,Administrator")]
        public ActionResult AddMember (int id)
        {
            List<ApplicationUser> posibleNewMembers = GetPossibleNewMembers();
            MemberToTeam fmm = new MemberToTeam
            {
                TeamId = id
            };
            var viewModel = new AddMemberToTeamViewModel
            {
                MemberToTeam = fmm,
                PossibleNewMembers = posibleNewMembers
            };
            return View("AddMember", viewModel);
        }
        [Authorize(Roles = "User,Membru,Organizator,Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveNewMember (MemberToTeam memberToTeam)
        {
            var viewModel = new AddMemberToTeamViewModel
            {
               MemberToTeam = memberToTeam
            };
            if (!ModelState.IsValid)
                return View("AddMember", viewModel);

            _context.MemberToTeams.Add(memberToTeam);
            _context.SaveChanges();
            TempData["message"] = "Membrul a fost adaugat";
            return RedirectToAction("ShowTeam", new { id = memberToTeam.TeamId });
        }
        [Authorize(Roles = "User,Membru,Organizator,Administrator")]
        public ActionResult SaveMemberToTeam (MemberToTeam memberToTeam)
        {
            List<ApplicationUser> posibleNewMembers = GetPossibleNewMembers();
            var viewModel1 = new AddMemberToTeamViewModel
            {
                MemberToTeam = memberToTeam,
                PossibleNewMembers = posibleNewMembers
            };
            if (!ModelState.IsValid)
                return View("AddMember", viewModel1);

            _context.MemberToTeams.Add(memberToTeam);
            _context.SaveChanges();

            return RedirectToAction("Show", memberToTeam.TeamId);
        }
        [Authorize(Roles = "User,Membru,Organizator,Administrator")]
        [HttpDelete]
        public ActionResult DeleteTeam (int id)
        {
            var dbTeam = _context.Teams.SingleOrDefault(c => c.Id == id);
            if (dbTeam == null)
                return HttpNotFound();
            var projectId = dbTeam.ProjectId;
            _context.Teams.Remove(dbTeam);
            _context.SaveChanges();
            TempData["message"] = "Echipa-ul a fost stearsa cu succes.";
            return RedirectToAction("Show", "Project", new { id = projectId });
        }
        [NonAction]
        public List<ApplicationUser> GetPossibleNewMembers()
        {
            List<ApplicationUser> allMembers = _context.MemberToTeams.Select(c => c.Member).ToList();
            List<ApplicationUser> allUsers = _context.Users.ToList();
            List<ApplicationUser> possibleNewMembers = allUsers.Except(allMembers).ToList();
            List<ApplicationUser> organizers = _context.Projects.Include("Users").Select(c => c.User).ToList();
            possibleNewMembers = possibleNewMembers.Except(organizers).ToList();
            string userId = User.Identity.GetUserId();
            ApplicationUser currentUser = _context.Users.SingleOrDefault(c => c.Id == userId);
            ApplicationUser admin = _context.Users.SingleOrDefault(c => c.UserName == "admin@manageraplicatii.com");
            possibleNewMembers.Remove(currentUser);
            possibleNewMembers.Remove(admin);
            return possibleNewMembers;
        }
    }
}