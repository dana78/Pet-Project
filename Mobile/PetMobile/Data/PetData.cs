using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace PetMobile.Data
{
    [Table("Pet")]
    public class PetData
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Breed { get; set; }
        public string Color { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<DiseaseData> Diseases { get; set; }

        //  TODO: Add vaccinations and histories
    }
}
