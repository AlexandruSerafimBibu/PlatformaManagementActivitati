using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlatformaManagementActivitati.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Titlul este obligatoriu.")]
        [StringLength(30, ErrorMessage = "Titlul nu poate avea mai mult de 30 de caractere.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Descrierea este obligatorie")]
        public string Descriere { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public DateTime StartDate { get; set; }
        [Range(typeof(DateTime), "10/5/2020", "1/1/2022", ErrorMessage = "Value for {0} must be between {1} and {2}")]
        [Display(Name = "Data finalizarii")]
        public DateTime DeadlineDate { get; set; }
    }
}