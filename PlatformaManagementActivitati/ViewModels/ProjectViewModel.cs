using PlatformaManagementActivitati.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlatformaManagementActivitati.ViewModels
{
    public class ProjectViewModel
    {
        public Project Project { get; set; }
        public bool EsteAdmin { get; set; }
        public bool EsteMembru { get; set; }
        public bool AfisareButoane { get; set; }
        public string UtilizatorCurent { get; set; }
        public IEnumerable<Assignment> Assignments { get; set; }
        public IEnumerable<Team> Teams { get; set; }
    }
}