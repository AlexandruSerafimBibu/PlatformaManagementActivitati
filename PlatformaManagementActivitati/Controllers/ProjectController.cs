using Microsoft.AspNet.Identity;
using PlatformaManagementActivitati.Models;
using PlatformaManagementActivitati.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlatformaManagementActivitati.Controllers
{
    public class ProjectController : Controller
    {
        private ApplicationDbContext _context;

        public ProjectController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        [Authorize(Roles = "User,Membru,Organizator,Administrator")]
        public ActionResult Index()
        {
            var projects = _context.Projects.Include("User");

            if (TempData.ContainsKey("message"))
                ViewBag.message = TempData["message"].ToString();

            return View(projects);
        }
        [Authorize(Roles = "User,Membru,Organizator,Administrator")]
        public ActionResult New()
        {
            var viewModel = new Project
            {
                UserId = User.Identity.GetUserId(),
                StartDate = DateTime.Now
            };
            return View("New", viewModel);
        }
        [Authorize(Roles = "User,Membru,Organizator,Administrator")]

        public ActionResult Show(int id)
        {
            var viewModel = new ProjectViewModel
            {
                Project = _context.Projects.Single(c => c.Id == id)
            };
            if (viewModel == null)
                return HttpNotFound();
            viewModel.UtilizatorCurent = User.Identity.GetUserId();
            viewModel.EsteAdmin = User.IsInRole("Administrator");
            viewModel.AfisareButoane = false;
            viewModel.EsteMembru = false;
            var userId = User.Identity.GetUserId();
            var membersList = _context.MemberToTeams.Where(c => c.Team.ProjectId == id).ToList();
            var member = _context.MemberToTeams.SingleOrDefault(c => c.MemberId == userId && c.Team.ProjectId == id);
            if (member != null && membersList.Contains(member))
                viewModel.EsteMembru = true;
            if (viewModel.Project.UserId == userId || User.IsInRole("Administrator"))
            {
                viewModel.AfisareButoane = true;
            }
            viewModel.Teams = _context.Teams.Where(c => c.ProjectId == viewModel.Project.Id);
            return View("Show", viewModel);
        }
        public ActionResult Edit(int id)
        {
            var viewModel = _context.Projects.Single(c => c.Id == id);

            if (viewModel == null)
                return HttpNotFound();

            if (viewModel.UserId == User.Identity.GetUserId() || User.IsInRole("Administrator"))
            {
                return View("Edit", viewModel);
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra unui proiect care nu va apartine.";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        [Authorize(Roles = "User,Membru,Organizator,Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult SaveNew(Project project)
        {
            if (!ModelState.IsValid)
                return View("New", project);

            _context.Projects.Add(project);
            _context.SaveChanges();
            TempData["message"] = "Proiectul a fost adaugat.";
            return RedirectToAction("Index", "Project");
        }
        [HttpPost]
        [Authorize(Roles = "User,Membru,Organizator,Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult SaveEdit(Project project)
        {
            if (!ModelState.IsValid)
                return View("Edit", project);
            
            var dbProject = _context.Projects.Single(c => c.Id == project.Id);
            dbProject.Title = project.Title;
            dbProject.Descriere = project.Descriere;
            dbProject.DeadlineDate = project.DeadlineDate;

            _context.SaveChanges();
            TempData["message"] = "Proiectul a fost modificiat.";
            return RedirectToAction("Index", "Project");
        }

        [HttpDelete]
        public ActionResult Detele(int id)
        {
            var dbProject = _context.Projects.SingleOrDefault(c => c.Id == id);
            if (dbProject == null)
                return HttpNotFound();
            _context.Projects.Remove(dbProject);
            _context.SaveChanges();
            TempData["message"] = "Proiectul a fost sters cu succes.";
            return RedirectToAction("Index", "Project");
        }
    }
}