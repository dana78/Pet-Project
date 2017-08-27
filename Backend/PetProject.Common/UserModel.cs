using PetProject.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Common
{
    /// <summary>
    /// User credentials for the app
    /// </summary>
    public class Credentials
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Encrypted password
        /// </summary>
        [Required]
        public string Password { get; set; }
    }

    public class UserRoleInfo
    {
        /// <summary>
        /// User identifier
        /// </summary>
        public int Id { get; set; }
        public int? OwnerId { get; set; }
        public int? VetId { get; set; }
        public int? ClinicId { get; set; }
        public Owner Owner { get; set; }
        public Vet Vet { get; set; }
        public Clinic Clinic { get; set; }
    }
}
