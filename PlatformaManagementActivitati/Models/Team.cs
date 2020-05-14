using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlatformaManagementActivitati.Models
{
    public class Team
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Numele este obligatoriu")]
        public string Name { get; set; }
        public Project Project { get; set; }
        [Required]
        public int ProjectId { get; set; }
    }
}