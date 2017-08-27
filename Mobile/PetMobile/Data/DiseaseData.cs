using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;

namespace PetMobile.Data
{
    [Table("Disease")]
    public class DiseaseData
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DiagnosticDate { get; set; }

        [ForeignKey(typeof(PetData))]
        public int PetId { get; set; }
    }
}
