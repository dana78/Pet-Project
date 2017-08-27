using System.ComponentModel.DataAnnotations;

namespace PetProject.Common
{
    /// <summary>
    /// Clinic request model
    /// </summary>
    public class ClinicRM
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        public string RUC { get; set; }
        public string Address { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string Phone { get; set; }
    }
}
