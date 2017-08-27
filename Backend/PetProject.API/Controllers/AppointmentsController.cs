using PetProject.Common;
using PetProject.Entities;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace PetProject.API.Controllers
{
    /// <summary>
    /// Appointment API
    /// </summary>
    public class AppointmentsController : ApiController
    {
        SQLMappingDataContext context = new SQLMappingDataContext();


        /// <summary>
        /// Returns the appointment with the given id
        /// </summary>
        /// <param name="id">Appointment id</param>
        /// <returns>Appointment</returns>
        [HttpGet]
        [Route("api/appointments/{id}")]
        [ResponseType(typeof(Appointment))]
        public IHttpActionResult GetAppointment(int id)
        {
            try
            {
                var appointment = context.Appointments.FirstOrDefault(a => a.idAppointment == id);
                return Ok(appointment);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        /// Registers a new appointment
        /// </summary>
        /// <param name="appointment">Appointment info</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/appointments")]
        [ResponseType(typeof(Appointment))]
        public IHttpActionResult PostAppointment([FromBody]AppointmentRM appointment)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                //  Search for pet
                var pet = context.Pets.FirstOrDefault(p => p.idPet == appointment.PetId);
                if (pet == null)
                    return NotFound();

                //  Search for clinic
                var clinic = context.Clinics.FirstOrDefault(c => c.idClinic == appointment.ClinicId);
                if (clinic == null)
                {
                    //  If clinic is not found, search for vet
                    appointment.ClinicId = null;
                    var vet = context.Vets.FirstOrDefault(v => v.idVet == appointment.VetId);
                    if (vet == null)
                        return NotFound();
                }
                else
                {
                    appointment.VetId = null;
                }

                context.Connection.Open();
                using (context.Transaction = context.Connection.BeginTransaction())
                {
                    Appointment newAppointment = MapAppointmentFromRM(appointment);
                    context.Appointments.InsertOnSubmit(newAppointment);
                    context.SubmitChanges(ConflictMode.FailOnFirstConflict);

                    DetailAppointment newDetail = MapDetailAppointment(appointment, newAppointment);
                    context.DetailAppointments.InsertOnSubmit(newDetail);
                    context.SubmitChanges(ConflictMode.FailOnFirstConflict);

                    context.Transaction.Commit();
                    return Ok(newAppointment);
                }
            }
            catch (Exception e)
            {
                context.Transaction?.Rollback();
                return InternalServerError(e);
            }

        }


        /// <summary>
        /// Confirms an appointment
        /// </summary>
        /// <param name="id">Appointment ID</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/appointments/{id}/confirm")]
        public IHttpActionResult ConfirmAppointment(int id)
        {
            try
            {
                var appointment = context.Appointments.FirstOrDefault(a => a.idAppointment == id);
                if (appointment == null) return NotFound();

                appointment.confirmed = true;
                context.SubmitChanges();

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        /// Changes the date of the appointment with the given ID
        /// </summary>
        /// <param name="id">Appointment Id</param>
        /// <param name="date">New date</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/appointments/{id}/ChangeDate")]
        public IHttpActionResult ChangeDate(int id, DateTime date)
        {
            try
            {
                var appointment = context.Appointments.FirstOrDefault(a => a.idAppointment == id);
                if (appointment == null) return NotFound();

                appointment.appointmentDate = date;
                context.SubmitChanges();

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        #region Helper methods

        private DetailAppointment MapDetailAppointment(AppointmentRM appointment, Appointment newAppointment)
        {
            return new DetailAppointment()
            {
                idAppointment = newAppointment.idAppointment,
                idClinic = appointment.ClinicId,
                idVet = appointment.VetId
            };
        }

        private Appointment MapAppointmentFromRM(AppointmentRM appointment)
        {
            return new Appointment()
            {
                idPet = appointment.PetId,
                appointmentDate = appointment.Date,
                title = appointment.Title,
                confirmed = false
            };
        }

        #endregion
    }
}
