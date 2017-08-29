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
    /// Pets API
    /// </summary>
    public class PetsController : ApiController
    {
        SQLMappingDataContext db = new SQLMappingDataContext();


        /// <summary>
        /// Registers a new pet
        /// </summary>
        /// <param name="pet">New pet</param>
        /// <returns>Pet info</returns>
        [HttpPost]
        [Route("api/pets")]
        [ResponseType(typeof(Pet))]
        public IHttpActionResult PostPet([FromBody]PetRM pet)
        {
            if (!ModelState.IsValid || pet == null)
                return BadRequest();

            try
            {
                var owner = db.Owners.FirstOrDefault(o => o.idOwner == pet.OwnerId);
                if (owner == null) return NotFound();

                Pet newPet = MapPetFromRM(pet);
                db.Pets.InsertOnSubmit(newPet);
                db.SubmitChanges(System.Data.Linq.ConflictMode.FailOnFirstConflict);

                return Ok(pet);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }


        /// <summary>
        /// Returns a pet info with the given pet ID
        /// </summary>
        /// <param name="petId">Pet ID</param>
        /// <returns>Pet info</returns>
        [HttpGet]
        [Route("api/pets/{petId}")]
        [ResponseType(typeof(Pet))]
        public IHttpActionResult GetPetById(int petId)
        {
            try
            {
                var pet = db.Pets.FirstOrDefault(p => p.idPet == petId);
                if (pet == null)
                    return NotFound();

                return Ok(pet);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }


        /// <summary>
        /// Updates pet info
        /// </summary>
        /// <param name="pet"></param>
        /// <returns>New pet info</returns>
        [HttpPut]
        [Route("api/pets")]
        [ResponseType(typeof(Pet))]
        public IHttpActionResult UpdatePet(Pet pet)
        {
            if (!ModelState.IsValid || pet == null)
                return BadRequest();

            try
            {
                var petFound = db.Pets.FirstOrDefault(p => p.idPet == pet.idPet);
                if (petFound == null)
                    return NotFound();

                petFound.firstname = pet.firstname;
                petFound.lastname = pet.lastname;
                petFound.breed = pet.breed;
                petFound.color = pet.color;
                petFound.birthday = pet.birthday;

                db.SubmitChanges(System.Data.Linq.ConflictMode.FailOnFirstConflict);
                return Ok(petFound);
            }
            catch(Exception e)
            {
                return InternalServerError(e);
            }
        }


        /// <summary>
        /// Returns the list of clinic histories within the given pet id
        /// </summary>
        /// <param name="petId">Pet id</param>
        /// <returns>Ordered clinic histories</returns>
        [HttpGet]
        [Route("api/pets/{petId}/histories")]
        [ResponseType(typeof(ClinicHistory[]))]
        public IHttpActionResult GetClinicHistories(int petId)
        {
            try
            {
                var pet = db.Pets.FirstOrDefault(p => p.idPet == petId);
                if (pet == null)
                    return NotFound();

                var histories = db.ClinicHistories
                                    .Where(h => h.idPet == pet.idPet)
                                    .OrderByDescending(h => h.arrivalDate);
                return Ok(histories);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        private Pet MapPetFromRM(PetRM pet)
        {
            return new Pet()
            {
                birthday = pet.Birthday,
                breed = pet.Breed,
                color = pet.Color,
                firstname = pet.Firstname,
                lastname = pet.Lastname,
                idOwner = pet.OwnerId
            };
        }
    }
}
