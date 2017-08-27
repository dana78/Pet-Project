using System;
using System.ComponentModel.DataAnnotations;

namespace PetProject.Common
{
    public class OwnerRM
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
    }
}
