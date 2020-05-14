using PlatformaManagementActivitati.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlatformaManagementActivitati.ViewModels
{
    public class ShowTaskViewModel
    {
        public Assignment Assignment { get; set; }
        public bool EsteAdmin { get; set; }
        public bool AfisareButoane { get; set; }
        public bool AfisareModifica { get; set; }
        public string UtilizatorCurent { get; set; }
    }
}