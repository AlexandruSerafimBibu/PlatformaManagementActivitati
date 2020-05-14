using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlatformaManagementActivitati.Models
{
    public class Assignment
    {
        public int Id { get; set; }
       
        [Range(0, 2, ErrorMessage = "0 - neinceput. 1- inceput. 2 - finalizat")]
        public int? Status { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Descriere { get; set; }
        public DateTime DataStart { get; set; }
        [Range(typeof(DateTime), "5/14/2020", "1/1/2022", ErrorMessage = "Value for {0} must be between {1} and {2}")]
        [Display(Name = "Data finalizarii")]
        public DateTime? DataFinalizare { get; set; }
        public ApplicationUser UserResponsabil { get; set; }
        [Display(Name = "Membru")]
        [Required(ErrorMessage = "Membrul responsabil este obligatoriu")]
        public string UserResponsabilId { get; set; }
        public Team Team { get; set; }
        [Required]
        public int TeamId { get; set; }
    }
}