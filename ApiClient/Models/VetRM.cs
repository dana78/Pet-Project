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
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace PetApiClient
{
    /// <summary>
    /// VetRM
    /// </summary>
    [DataContract]
    public partial class VetRM :  IEquatable<VetRM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VetRM" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected VetRM() { }        
        
        /// <summary>
        /// Gets or Sets UserId
        /// </summary>
        [DataMember(Name="UserId", EmitDefaultValue=false)]
        public int? UserId { get; set; }
        /// <summary>
        /// Gets or Sets Firstname
        /// </summary>
        [DataMember(Name="Firstname", EmitDefaultValue=false)]
        public string Firstname { get; set; }
        /// <summary>
        /// Gets or Sets Lastname
        /// </summary>
        [DataMember(Name="Lastname", EmitDefaultValue=false)]
        public string Lastname { get; set; }
        /// <summary>
        /// Gets or Sets RUC
        /// </summary>
        [DataMember(Name="RUC", EmitDefaultValue=false)]
        public string RUC { get; set; }
        /// <summary>
        /// Gets or Sets Phone
        /// </summary>
        [DataMember(Name="Phone", EmitDefaultValue=false)]
        public string Phone { get; set; }
        /// <summary>
        /// Gets or Sets LicenseCode
        /// </summary>
        [DataMember(Name="LicenseCode", EmitDefaultValue=false)]
        public string LicenseCode { get; set; }
        /// <summary>
        /// Gets or Sets LicenseDate
        /// </summary>
        [DataMember(Name="LicenseDate", EmitDefaultValue=false)]
        public DateTime? LicenseDate { get; set; }
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class VetRM {\n");
            sb.Append("  UserId: ").Append(UserId).Append("\n");
            sb.Append("  Firstname: ").Append(Firstname).Append("\n");
            sb.Append("  Lastname: ").Append(Lastname).Append("\n");
            sb.Append("  RUC: ").Append(RUC).Append("\n");
            sb.Append("  Phone: ").Append(Phone).Append("\n");
            sb.Append("  LicenseCode: ").Append(LicenseCode).Append("\n");
            sb.Append("  LicenseDate: ").Append(LicenseDate).Append("\n");
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
            return this.Equals(obj as VetRM);
        }

        /// <summary>
        /// Returns true if VetRM instances are equal
        /// </summary>
        /// <param name="other">Instance of VetRM to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(VetRM other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.UserId == other.UserId ||
                    this.UserId != null &&
                    this.UserId.Equals(other.UserId)
                ) && 
                (
                    this.Firstname == other.Firstname ||
                    this.Firstname != null &&
                    this.Firstname.Equals(other.Firstname)
                ) && 
                (
                    this.Lastname == other.Lastname ||
                    this.Lastname != null &&
                    this.Lastname.Equals(other.Lastname)
                ) && 
                (
                    this.RUC == other.RUC ||
                    this.RUC != null &&
                    this.RUC.Equals(other.RUC)
                ) && 
                (
                    this.Phone == other.Phone ||
                    this.Phone != null &&
                    this.Phone.Equals(other.Phone)
                ) && 
                (
                    this.LicenseCode == other.LicenseCode ||
                    this.LicenseCode != null &&
                    this.LicenseCode.Equals(other.LicenseCode)
                ) && 
                (
                    this.LicenseDate == other.LicenseDate ||
                    this.LicenseDate != null &&
                    this.LicenseDate.Equals(other.LicenseDate)
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
                if (this.UserId != null)
                    hash = hash * 59 + this.UserId.GetHashCode();
                if (this.Firstname != null)
                    hash = hash * 59 + this.Firstname.GetHashCode();
                if (this.Lastname != null)
                    hash = hash * 59 + this.Lastname.GetHashCode();
                if (this.RUC != null)
                    hash = hash * 59 + this.RUC.GetHashCode();
                if (this.Phone != null)
                    hash = hash * 59 + this.Phone.GetHashCode();
                if (this.LicenseCode != null)
                    hash = hash * 59 + this.LicenseCode.GetHashCode();
                if (this.LicenseDate != null)
                    hash = hash * 59 + this.LicenseDate.GetHashCode();
                return hash;
            }
        }

    }

}
