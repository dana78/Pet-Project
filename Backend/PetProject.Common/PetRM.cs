using System;
using System.ComponentModel.DataAnnotations;

namespace PetProject.Common
{
    public class PetRM
    {
        [Required]
        public int OwnerId { get; set; }
        [Required]
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        /// <summary>
        /// True: Male
        /// False: Female
        /// </summary>
        public bool? Sex { get; set; }
        public string Breed { get; set; }
        public string Color { get; set; }
    }
}
