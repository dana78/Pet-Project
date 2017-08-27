using PetProject.Common;
using PetProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace PetProject.API.Controllers
{
    /// <summary>
    /// Histories API
    /// </summary>
    public class HistoriesController : ApiController
    {
        SQLMappingDataContext context = new SQLMappingDataContext();


        /// <summary>
        /// Register a new clinic history
        /// </summary>
        /// <param name="history">New clinic history</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/histories")]
        [ResponseType(typeof(ClinicHistory))]
        public IHttpActionResult RegisterNewClinicHistory(ClinicHistoryRM history)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var pet = context.Pets.FirstOrDefault(p => p.idPet == history.PetId);
                if (pet == null)
                    NotFound();

                var vet = context.Vets.FirstOrDefault(v => v.idVet == history.VetId);
                if (vet == null)
                    NotFound();

                if (history.ClinicId.HasValue)
                {
                    var clinic = context.Clinics.FirstOrDefault(c => c.idClinic == history.ClinicId);
                    if (clinic == null)
                        return NotFound();
                }

                ClinicHistory newHistory = MapHistoryFromRM(history);
                context.ClinicHistories.InsertOnSubmit(newHistory);
                context.SubmitChanges(System.Data.Linq.ConflictMode.FailOnFirstConflict);
                return Ok(newHistory);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        private ClinicHistory MapHistoryFromRM(ClinicHistoryRM history)
        {
            return new ClinicHistory()
            {
                idPet = history.PetId,
                idClinic = history.ClinicId,
                idVet = history.VetId,
                anamnesis = history.Anamnesis,
                arrivalDate = history.ArrivalDate,
                dischargeDate = history.DischargeDate,
                clinicalSign = history.ClinicalSign,
                diagnostic = history.Diagnostic,
                treatment = history.Treatment,
                observations = history.Observations
            };
        }


        /// <summary>
        /// Updates a given clinic history
        /// </summary>
        /// <param name="history">Clinic history</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/histories")]
        public IHttpActionResult UpdateClinicHistory(ClinicHistory history)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var historyFound = context.ClinicHistories.FirstOrDefault(c => c.idHistory == history.idHistory);
                if (historyFound == null)
                    return NotFound();

                historyFound.anamnesis = history.anamnesis;
                historyFound.arrivalDate = history.arrivalDate;
                historyFound.clinicalSign = history.clinicalSign;
                historyFound.diagnostic = history.diagnostic;
                historyFound.dischargeDate = historyFound.dischargeDate;
                historyFound.observations = historyFound.observations;
                historyFound.treatment = historyFound.treatment;
                historyFound.Tests = history.Tests;

                context.SubmitChanges(System.Data.Linq.ConflictMode.FailOnFirstConflict);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
