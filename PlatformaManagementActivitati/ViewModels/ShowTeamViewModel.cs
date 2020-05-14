using PlatformaManagementActivitati.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlatformaManagementActivitati.ViewModels
{
    public class ShowTeamViewModel
    {
        public IEnumerable<Assignment> Assignments { get; set; }
        public string UtilizatorCurent { get; set; }
        public bool EsteAdmin { get; set; }
        public bool AfisareButoane { get; set; }
        public bool AfisareButoane_2 { get; set; }
        public Team Team { get; set; }
        public ICollection <String> Members { get; set; }
        public IEnumerable <ApplicationUser> PosibleNewMembers { get; set; }
        //public ApplicationUser NewMember { get; set; }
        public MemberToTeam NewMemberToTeam { get; set; }
    }
}