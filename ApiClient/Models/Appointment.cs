/* 
 * API for Pet Project
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: v1
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PetApiClient
{

    /// <summary>
    /// Appointment
    /// </summary>
    [DataContract]
    public class Appointment :  IEquatable<Appointment>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Appointment" /> class.
        /// </summary>
        /// <param name="IdAppointment">IdAppointment.</param>
        /// <param name="AppointmentDate">AppointmentDate.</param>
        /// <param name="Title">Title.</param>
        /// <param name="IdPet">IdPet.</param>
        /// <param name="Confirmed">Confirmed.</param>
        /// <param name="DetailAppointments">DetailAppointments.</param>
        /// <param name="Pet">Pet.</param>
        public Appointment(int? IdAppointment = default(int?), DateTime? AppointmentDate = default(DateTime?), string Title = default(string), int? IdPet = default(int?), bool? Confirmed = default(bool?), List<DetailAppointment> DetailAppointments = default(List<DetailAppointment>), Pet Pet = default(Pet))
        {
            this.IdAppointment = IdAppointment;
            this.AppointmentDate = AppointmentDate;
            this.Title = Title;
            this.IdPet = IdPet;
            this.Confirmed = Confirmed;
            this.DetailAppointments = DetailAppointments;
            this.Pet = Pet;
        }
        
        /// <summary>
        /// Gets or Sets IdAppointment
        /// </summary>
        [DataMember(Name="idAppointment", EmitDefaultValue=false)]
        public int? IdAppointment { get; set; }
        /// <summary>
        /// Gets or Sets AppointmentDate
        /// </summary>
        [DataMember(Name="appointmentDate", EmitDefaultValue=false)]
        public DateTime? AppointmentDate { get; set; }
        /// <summary>
        /// Gets or Sets Title
        /// </summary>
        [DataMember(Name="title", EmitDefaultValue=false)]
        public string Title { get; set; }
        /// <summary>
        /// Gets or Sets IdPet
        /// </summary>
        [DataMember(Name="idPet", EmitDefaultValue=false)]
        public int? IdPet { get; set; }
        /// <summary>
        /// Gets or Sets Confirmed
        /// </summary>
        [DataMember(Name="confirmed", EmitDefaultValue=false)]
        public bool? Confirmed { get; set; }
        /// <summary>
        /// Gets or Sets Confirmed
        /// </summary>
        [DataMember(Name = "attended", EmitDefaultValue = false)]
        public bool? Attended { get; set; }
        /// <summary>
        /// Gets or Sets DetailAppointments
        /// </summary>
        [DataMember(Name="DetailAppointments", EmitDefaultValue=false)]
        public List<DetailAppointment> DetailAppointments { get; set; }
        /// <summary>
        /// Gets or Sets Pet
        /// </summary>
        [DataMember(Name="Pet", EmitDefaultValue=false)]
        public Pet Pet { get; set; }
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Appointment {\n");
            sb.Append("  IdAppointment: ").Append(IdAppointment).Append("\n");
            sb.Append("  AppointmentDate: ").Append(AppointmentDate).Append("\n");
            sb.Append("  Title: ").Append(Title).Append("\n");
            sb.Append("  IdPet: ").Append(IdPet).Append("\n");
            sb.Append("  Confirmed: ").Append(Confirmed).Append("\n");
            sb.Append("  DetailAppointments: ").Append(DetailAppointments).Append("\n");
            sb.Append("  Pet: ").Append(Pet).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            return this.Equals(obj as Appointment);
        }

        /// <summary>
        /// Returns true if Appointment instances are equal
        /// </summary>
        /// <param name="other">Instance of Appointment to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Appointment other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.IdAppointment == other.IdAppointment ||
                    this.IdAppointment != null &&
                    this.IdAppointment.Equals(other.IdAppointment)
                ) && 
                (
                    this.AppointmentDate == other.AppointmentDate ||
                    this.AppointmentDate != null &&
                    this.AppointmentDate.Equals(other.AppointmentDate)
                ) && 
                (
                    this.Title == other.Title ||
                    this.Title != null &&
                    this.Title.Equals(other.Title)
                ) && 
                (
                    this.IdPet == other.IdPet ||
                    this.IdPet != null &&
                    this.IdPet.Equals(other.IdPet)
                ) && 
                (
                    this.Confirmed == other.Confirmed ||
                    this.Confirmed != null &&
                    this.Confirmed.Equals(other.Confirmed)
                ) && 
                (
                    this.DetailAppointments == other.DetailAppointments ||
                    this.DetailAppointments != null &&
                    this.DetailAppointments.SequenceEqual(other.DetailAppointments)
                ) && 
                (
                    this.Pet == other.Pet ||
                    this.Pet != null &&
                    this.Pet.Equals(other.Pet)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            // credit: http://stackoverflow.com/a/263416/677735
            unchecked // Overflow is fine, just wrap
            {
                int hash = 41;
                // Suitable nullity checks etc, of course :)
                if (this.IdAppointment != null)
                    hash = hash * 59 + this.IdAppointment.GetHashCode();
                if (this.AppointmentDate != null)
                    hash = hash * 59 + this.AppointmentDate.GetHashCode();
                if (this.Title != null)
                    hash = hash * 59 + this.Title.GetHashCode();
                if (this.IdPet != null)
                    hash = hash * 59 + this.IdPet.GetHashCode();
                if (this.Confirmed != null)
                    hash = hash * 59 + this.Confirmed.GetHashCode();
                if (this.DetailAppointments != null)
                    hash = hash * 59 + this.DetailAppointments.GetHashCode();
                if (this.Pet != null)
                    hash = hash * 59 + this.Pet.GetHashCode();
                return hash;
            }
        }
    }

}
