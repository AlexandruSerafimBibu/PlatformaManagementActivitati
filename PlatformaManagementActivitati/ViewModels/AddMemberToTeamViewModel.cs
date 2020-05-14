using PlatformaManagementActivitati.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlatformaManagementActivitati.ViewModels
{
    public class AddMemberToTeamViewModel
    {
        public ICollection<ApplicationUser> PossibleNewMembers { get; set; }
        public MemberToTeam MemberToTeam { get; set; }
    }
}