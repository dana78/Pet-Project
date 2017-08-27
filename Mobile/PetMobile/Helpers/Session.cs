using PetApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetMobile.Helpers
{
    public class Session
    {
        private static readonly Session _instance = new Session();
        public static Session Instance { get { return _instance; } }
        private Session() { }

        public Owner Owner { get; set; }
        public Vet Vet { get; set; }
    }
}
