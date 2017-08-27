using System;
using System.ComponentModel.DataAnnotations;

namespace PetProject.Common
{
    /// <summary>
    /// Appointment request model
    /// </summary>
    public class AppointmentRM
    {
        [Required]
        public int PetId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Title { get; set; }
        public int? ClinicId { get; set; }
        public int? VetId { get; set; }
    }
}
