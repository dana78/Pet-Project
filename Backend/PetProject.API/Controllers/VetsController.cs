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
    /// Vets API
    /// </summary>
    public class VetsController : ApiController
    {
        SQLMappingDataContext db = new SQLMappingDataContext();


        /// <summary>
        /// Returns the vet profile with the given ID
        /// </summary>
        /// <param name="id">Vet ID</param>
        /// <returns>Vet profile</returns>
        [HttpGet]
        [Route("api/vets/{id}")]
        [ResponseType(typeof(Vet))]
        public IHttpActionResult GetVetProfile(int id)
        {
            try
            {
                var vetFound = db.Vets.FirstOrDefault(v => v.idVet == id);
                if (vetFound == null)
                    return NotFound();

                return Ok(vetFound);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }


        /// <summary>
        /// Registers a new vet
        /// </summary>
        /// <param name="vet"></param>
        /// <returns>New vet profile</returns>
        [HttpPost]
        [Route("api/vets")]
        [ResponseType(typeof(Vet))]
        public IHttpActionResult PostVetProfile([FromBody]VetRM vet)
        {
            if (!ModelState.IsValid || vet == null)
                return BadRequest();

            db.Connection.Open();
            using (db.Transaction = db.Connection.BeginTransaction())
            {
                try
                {
                    //  Check if user exists
                    var user = db.Users.FirstOrDefault(u => u.idUser == vet.UserId);
                    if (user == null)
                    {
                        db.Transaction.Rollback();
                        return NotFound();
                    }

                    //  Insert new vet
                    Vet newVet = MapVetFromRM(vet);
                    db.Vets.InsertOnSubmit(newVet);
                    db.SubmitChanges(System.Data.Linq.ConflictMode.FailOnFirstConflict);

                    //  Update user table to reference the new vet
                    user.Vet = newVet;
                    db.SubmitChanges(System.Data.Linq.ConflictMode.FailOnFirstConflict);

                    //  If everything is OK, commit
                    db.Transaction.Commit();

                    return Ok(newVet);
                }
                catch (Exception e)
                {
                    //  Rollback if something goes wrong
                    db.Transaction.Rollback();
                    return InternalServerError(e);
                }
            }
        }

        //[HttpPut]
        //[Route("api/vets/{id}")]
        //public IHttpActionResult PutVetProfile([FromBody] VetRM vet)
        //{
            
        //}


        /// <summary>
        /// Returns a list of appointments
        /// </summary>
        /// <param name="vetId">Vet id</param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(Appointment[]))]
        [Route("api/vets/{vetId}/appointments")]
        public IHttpActionResult GetAppointments(int vetId)
        {
            try
            {
                var vet = db.Vets.FirstOrDefault(v => v.idVet == vetId);
                if (vet == null)
                    return NotFound();

                var details = db.DetailAppointments.Where(d => d.idVet == vetId);
                if (!details.Any())
                    return Ok(Enumerable.Empty<Appointment>());

                var appointments = from d in details
                                   join a in db.Appointments 
                                   on d.idAppointment equals a.idAppointment
                                   where d.idVet == vetId
                                   select a;
                return Ok(appointments);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }


        /// <summary>
        /// Returns a the list of registered vets
        /// </summary>
        /// <param name="lat">Center point latitude</param>
        /// <param name="lon">Center point longitude</param>
        /// <param name="radius">Radius</param>
        /// <returns>Found vets</returns>
        [HttpGet]
        [Route("api/vets")]
        [ResponseType(typeof(Vet[]))]
        public IHttpActionResult GetVets(double lat = 0, double lon = 0, double radius = 100)
        {
            if (lat == 0 || lon == 0)
            {
                return Ok(db.Vets);
            }
            else
            {
                List<Vet> vets = new List<Vet>();
                foreach (var vet in db.Vets)
                {
                    double distance = Helpers.Instance.DistanceInMiles(
                                Convert.ToDouble(vet.latitude),
                                Convert.ToDouble(vet.longitude),
                                lat, lon);
                    if (distance < radius)
                        vets.Add(vet);
                }
                return Ok(vets);
            }
        }

        private Vet MapVetFromRM(VetRM vet)
        {
            return new Vet()
            {
                firstname = vet.Firstname,
                lastname = vet.Lastname,
                licenseCode = vet.LicenseCode,
                licenseDate = vet.LicenseDate,
                RUC = vet.RUC,
                phone = vet.Phone
            };
        }
    }
}
