using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlatformaManagementActivitati.Models
{
    public class MemberToTeam
    {
        public int Id { get; set; }
        public string MemberId { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public ApplicationUser Member { get; set; }
    }
}