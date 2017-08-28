using PetApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetProject.Web.Models
{
    public class VetRegister : VetRM
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}