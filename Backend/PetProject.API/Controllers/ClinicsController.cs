using PetProject.Common;
using PetProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace PetProject.API.Controllers
{
    /// <summary>
    /// Clinics API
    /// </summary>
    public class ClinicsController : ApiController
    {
        SQLMappingDataContext context = new SQLMappingDataContext();


        /// <summary>
        /// Returns the appointments from the given clinic
        /// </summary>
        /// <param name="clinicId"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(Appointment[]))]
        [Route("api/clinics/{clinicId}/appointments")]
        public IHttpActionResult GetAppointments(int clinicId)
        {
            try
            {
                var clinic = context.Clinics.FirstOrDefault(c => c.idClinic == clinicId);
                if (clinic == null)
                    return NotFound();

                var details = context.DetailAppointments.Where(d => d.idClinic == clinicId);
                if (!details.Any())
                    return StatusCode(System.Net.HttpStatusCode.NoContent);

                var appointments =
                            from d in details
                            join a in context.Appointments
                            on d.idAppointment equals a.idAppointment
                            where d.idClinic == clinicId
                            select a;

                return Ok(appointments);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }


        /// <summary>
        /// Register a new employment contract
        /// </summary>
        /// <param name="clinicId">Clinic id</param>
        /// <param name="vetId">Hired vet id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/clinics/{clinicId}/vets")]
        public IHttpActionResult HireVet(int clinicId, [FromBody]int vetId)
        {
            try
            {
                var clinic = context.Clinics.FirstOrDefault(c => c.idClinic == clinicId);
                if (clinic == null)
                    return NotFound();

                var vet = context.Vets.FirstOrDefault(v => v.idVet == vetId);
                if (vet == null)
                    return NotFound();

                context.DetailVetClinics.InsertOnSubmit(new DetailVetClinic()
                {
                    Clinic = clinic,
                    Vet = vet,
                    startDate = DateTime.Now,
                    endDate = null
                });
                context.SubmitChanges(System.Data.Linq.ConflictMode.FailOnFirstConflict);

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

        }


        /// <summary>
        /// Deletes the contract between the clinic and vet
        /// </summary>
        /// <param name="clinicId">Clinic id</param>
        /// <param name="vetId">Fired vet id</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/clinics/{clinicId}/vets")]
        public IHttpActionResult FireVet(int clinicId, [FromBody]int vetId)
        {
            try
            {
                var clinic = context.Clinics.FirstOrDefault(c => c.idClinic == clinicId);
                if (clinic == null)
                    return NotFound();

                var vet = context.Vets.FirstOrDefault(v => v.idVet == vetId);
                if (vet == null)
                    return NotFound();

                var contracts = context.DetailVetClinics.Where(d => d.idClinic == clinic.idClinic && d.idVet == vet.idVet);
                var actualContract = contracts.FirstOrDefault(c => !c.endDate.HasValue || c.endDate.Value < DateTime.Now);
                if (actualContract == null)
                    return NotFound();

                actualContract.endDate = DateTime.Now;
                context.SubmitChanges(System.Data.Linq.ConflictMode.FailOnFirstConflict);

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

        }

        /// <summary>
        /// Returns the list of employed vets within a given clinic
        /// </summary>
        /// <param name="clinicId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/clinics/{clinicId}/vets")]
        [ResponseType(typeof(Vet[]))]
        public IHttpActionResult GetVets(int clinicId)
        {
            try
            {
                var clinic = context.Clinics.FirstOrDefault(c => c.idClinic == clinicId);
                if (clinic == null)
                    return NotFound();

                var employmentDetail = context.DetailVetClinics.Where(d => d.idClinic == clinic.idClinic);
                var employees = employmentDetail.Select(d => d.Vet);
                return Ok(employees);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }


        /// <summary>
        /// Returns a the list of registered clinics
        /// </summary>
        /// <param name="lat">Center point latitude</param>
        /// <param name="lon">Center point longitude</param>
        /// <param name="radius">Radius</param>
        /// <returns>Found clinics</returns>
        [HttpGet]
        [Route("api/clinics")]
        [ResponseType(typeof(Clinic[]))]
        public IHttpActionResult GetClinics(double lat = 0, double lon = 0, double radius = 100)
        {
            if (lat == 0 || lon == 0)
            {
                return Ok(context.Clinics);
            }
            else
            {
                List<Clinic> clinics = new List<Clinic>();
                foreach (var clinic in context.Clinics)
                {
                    double distance = Helpers.Instance.DistanceInMiles(
                                Convert.ToDouble(clinic.latitude),
                                Convert.ToDouble(clinic.longitude),
                                lat, lon);
                    if (distance < radius)
                        clinics.Add(clinic);
                }
                return Ok(clinics);
            }
        }


        /// <summary>
        /// Returns the clinic profile with the given ID
        /// </summary>
        /// <param name="id">Clinic profile ID</param>
        /// <returns>Clinic profile</returns>
        [HttpGet]
        [Route("api/clinics/{id}")]
        [ResponseType(typeof(Clinic))]
        public IHttpActionResult GetClinicProfile(int id)
        {
            try
            {
                var clinicFound = context.Clinics.FirstOrDefault(v => v.idClinic == id);
                if (clinicFound == null)
                    return NotFound();

                return Ok(clinicFound);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }


        /// <summary>
        /// Registers a new clinic
        /// </summary>
        /// <param name="clinic">Clinic</param>
        /// <returns>New clinic profile</returns>
        [HttpPost]
        [Route("api/clinics")]
        [ResponseType(typeof(Clinic))]
        public IHttpActionResult PostClinicProfile([FromBody]ClinicRM clinic)
        {
            if (!ModelState.IsValid || clinic == null)
                return BadRequest();

            context.Connection.Open();
            using (context.Transaction = context.Connection.BeginTransaction())
            {
                try
                {
                    //  Check if user exists
                    var user = context.Users.First(u => u.idUser == clinic.UserId);
                    if (user == null)
                    {
                        context.Transaction.Rollback();
                        return NotFound();
                    }

                    //  Insert new clinic
                    Clinic newClinic = MapClinicFromRM(clinic);
                    context.Clinics.InsertOnSubmit(newClinic);
                    context.SubmitChanges(System.Data.Linq.ConflictMode.FailOnFirstConflict);

                    //  Update user table to reference the new clinic
                    user.Clinic = newClinic;
                    context.SubmitChanges(System.Data.Linq.ConflictMode.FailOnFirstConflict);

                    //  If everything is OK, commit
                    context.Transaction.Commit();

                    return Ok(newClinic);
                }
                catch (Exception e)
                {
                    //  Rollback if something goes wrong
                    context.Transaction.Rollback();
                    return InternalServerError(e);
                }
            }
        }

        private Clinic MapClinicFromRM(ClinicRM clinic)
        {
            return new Clinic()
            {
                name = clinic.Name,
                RUC = clinic.RUC,
                address = clinic.Address,
                latitude = clinic.Latitude,
                longitude = clinic.Longitude,
                phone = clinic.Phone
            };
        }

    }
}
