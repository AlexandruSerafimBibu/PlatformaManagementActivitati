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
    public class TaskController : Controller
    {
        private ApplicationDbContext _context;
        public TaskController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        [Authorize(Roles = "User,Membru,Organizator,Administrator")]
        public ActionResult AddTask(int id)
        {
            //Condisera abordarea din licenta. Sa selectezi echipa si in functie de ea sa iti apara anumiti useri.
            List<ApplicationUser> possibleResponsibleUsers = getPossibleResponsibleUsers(id);
            if (possibleResponsibleUsers == null)
                return HttpNotFound();
            var viewModel = new AddTaskViewModel
            {
                PossibleResponsibleUsers = possibleResponsibleUsers,
                Assignment = new Assignment()
            };
            viewModel.Assignment.TeamId = id;
            viewModel.Assignment.DataStart = DateTime.Now;
            viewModel.Assignment.DataFinalizare = DateTime.Now;
            viewModel.Assignment.Status = 0;
            return View("AddTask", viewModel);
        }
        [Authorize(Roles = "User,Membru,Organizator,Administrator")]
        [HttpPost]
        public ActionResult SaveTask(Assignment assignment)
        {
            List<ApplicationUser> possibleResponsibleUsers = getPossibleResponsibleUsers(assignment.TeamId);
            if (possibleResponsibleUsers == null)
                return HttpNotFound();
            var viewModel1 = new AddTaskViewModel
            {
                PossibleResponsibleUsers = possibleResponsibleUsers,
                Assignment = assignment
            };
            if (assignment.Id == 0)
            {
                if (!ModelState.IsValid)
                    return View("AddTask", viewModel1);
                _context.Assignments.Add(assignment);
                _context.SaveChanges();
                TempData["message"] = "Task-ul a fost adaugat";
                return RedirectToAction("../Team/ShowTeam", new { id = assignment.TeamId });
            }
            if (!ModelState.IsValid)
                return View("Edit", assignment);
            var dbAssignment = _context.Assignments.SingleOrDefault(c => c.Id == assignment.Id);
            dbAssignment.Name = assignment.Name;
            dbAssignment.Status = assignment.Status;
            dbAssignment.Descriere = assignment.Descriere;
            dbAssignment.DataFinalizare = assignment.DataFinalizare;
            assignment.Team = _context.Teams.SingleOrDefault(c => c.Id == assignment.TeamId);
            assignment.UserResponsabil = _context.Users.SingleOrDefault(c => c.Id == assignment.UserResponsabilId);
            _context.SaveChanges();
            TempData["message"] = "Task-ul a fost modificat";
            return RedirectToAction("ShowTask",new { id = assignment.Id });
        }
        [Authorize(Roles = "User,Membru,Organizator,Administrator")]
        public ActionResult ShowTask(int id)
        {
            var assignment = _context.Assignments.SingleOrDefault(c => c.Id == id);
            assignment.Team = _context.Teams.SingleOrDefault(c => c.Id == assignment.TeamId);
            assignment.UserResponsabil = _context.Users.SingleOrDefault(c => c.Id == assignment.UserResponsabilId);
            var userId = _context.Projects.SingleOrDefault(c => c.Id == assignment.Team.ProjectId).UserId;
            var viewModel = new ShowTaskViewModel
            {
                Assignment = assignment

            };
            viewModel.EsteAdmin = User.IsInRole("Administrator");
            viewModel.AfisareButoane = false;
            viewModel.AfisareModifica = false;
            var userIdCurrent = User.Identity.GetUserId();

            if (userIdCurrent == assignment.UserResponsabilId)
                viewModel.AfisareModifica = true;
            if (userId == userIdCurrent || User.IsInRole("Administrator"))
                viewModel.AfisareButoane = true;
            
            return View("Show", viewModel);
        }
        [Authorize(Roles = "User,Membru,Organizator,Administrator")]
        public ActionResult EditTask(int id)
        {
            var viewModel = _context.Assignments.SingleOrDefault(c => c.Id == id);
            if (viewModel == null)
                return HttpNotFound();
            return View("Edit", viewModel);
        }
        [Authorize(Roles = "User,Membru,Organizator,Administrator")]
        [HttpDelete]
        public ActionResult DeleteTask(int id)
        {
            var dbTask = _context.Assignments.SingleOrDefault(c => c.Id == id);
            if (dbTask == null)
                return HttpNotFound();
            var teamId = dbTask.TeamId;
            _context.Assignments.Remove(dbTask);
            _context.SaveChanges();
            TempData["message"] = "Task-ul a fost sters cu succes.";
            return RedirectToAction("ShowTeam", "Team", new { id = teamId });
        }
        public List<ApplicationUser> getPossibleResponsibleUsers (int teamId)
        {
            List<ApplicationUser> users= _context.MemberToTeams.Where(c => c.TeamId == teamId).Select(c => c.Member).ToList();
            if (users == null)
                return null;
            return users;
        }
    }
}