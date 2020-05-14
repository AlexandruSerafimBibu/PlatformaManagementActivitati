using PlatformaManagementActivitati.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlatformaManagementActivitati.ViewModels
{
    public class AddTaskViewModel
    {
        public ICollection<Team> Teams { get; set; }
        public ICollection<ApplicationUser> PossibleResponsibleUsers { get; set; }
        public Assignment Assignment { get; set; }
    }
}