using System;
using System.ComponentModel.DataAnnotations;

namespace PetProject.Common
{
    public class ClinicHistoryRM
    {
        [Required]
        public int PetId { get; set; }
        public int? VetId { get; set; }
        public int? ClinicId { get; set; }        
        public string Anamnesis { get; set; }
        public string Treatment { get; set; }
        public string Diagnostic { get; set; }
        [Required]
        public string ClinicalSign { get; set; }
        public string Observations { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DischargeDate { get; set; }

    }
}
