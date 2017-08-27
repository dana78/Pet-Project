using PetApiClient;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetMobile.Data
{
    [Table("Owner")]
    public class OwnerData
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }

        public Owner ToOwner()
        {
            return new Owner(Id, Firstname, Lastname, Birthday, Phone);
        }
    }
}
