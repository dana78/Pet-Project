using System;
using System.ComponentModel.DataAnnotations;

namespace PetProject.Common
{
    /// <summary>
    /// Vet request model
    /// </summary>
    public class VetRM
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        public string RUC { get; set; }
        public string Phone { get; set; }
        public string LicenseCode { get; set; }
        public DateTime LicenseDate { get; set; }
    }
}
