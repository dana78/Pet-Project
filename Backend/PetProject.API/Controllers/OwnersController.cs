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
    /// Pet owners API
    /// </summary>
    public class OwnersController : ApiController
    {
        SQLMappingDataContext context = new SQLMappingDataContext();


        /// <summary>
        /// Gets the pet owner profile with the given ID
        /// </summary>
        /// <param name="id">Pet owner ID</param>
        /// <returns>Owner profile</returns>
        [HttpGet]
        [Route("api/owners/{id}")]
        [ResponseType(typeof(Owner))]
        public IHttpActionResult GetOwnerProfile(int id)
        {
            try
            {
                var ownerFound = context.Owners.FirstOrDefault(v => v.idOwner == id);
                if (ownerFound == null)
                    return NotFound();

                return Ok(ownerFound);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }


        /// <summary>
        /// Registers a new vet
        /// </summary>
        /// <param name="owner">Owner</param>
        /// <returns>New owner profile</returns>
        [HttpPost]
        [Route("api/owners")]
        [ResponseType(typeof(Owner))]
        public IHttpActionResult PostVetProfile([FromBody]OwnerRM owner)
        {
            if (!ModelState.IsValid || owner == null)
                return BadRequest();

            context.Connection.Open();
            using (context.Transaction = context.Connection.BeginTransaction())
            {
                try
                {
                    //  Check if user exists
                    var user = context.Users.First(u => u.idUser == owner.UserId);
                    if (user == null)
                    {
                        context.Transaction.Rollback();
                        return NotFound();
                    }

                    //  Insert new owner
                    Owner newOwner = PopulateVetFromRM(owner);
                    context.Owners.InsertOnSubmit(newOwner);
                    context.SubmitChanges(System.Data.Linq.ConflictMode.FailOnFirstConflict);

                    //  Update user table to reference the new owner
                    user.Owner = newOwner;
                    context.SubmitChanges(System.Data.Linq.ConflictMode.FailOnFirstConflict);

                    //  If everything is OK, commit
                    context.Transaction.Commit();

                    return Ok(newOwner);
                }
                catch (Exception e)
                {
                    //  Rollback if something goes wrong
                    context.Transaction.Rollback();
                    return InternalServerError(e);
                }
            }
        }


        /// <summary>
        /// Updates owner info
        /// </summary>
        /// <param name="owner">New owner profile</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/owners")]
        public IHttpActionResult UpdateOwnerProfile(Owner owner)
        {
            try
            {
                var ownerFound = context.Owners.FirstOrDefault(o => o.idOwner == owner.idOwner);
                if (ownerFound == null)
                    return NotFound();

                ownerFound.firstname = owner.firstname;
                ownerFound.lastname = owner.lastname;
                ownerFound.phone = owner.phone;
                ownerFound.birthday = owner.birthday;

                context.SubmitChanges();

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }


        /// <summary>
        /// Returns a list of pets with the given owner ID
        /// </summary>
        /// <param name="ownerId">Owner ID</param>
        /// <returns>List of pets</returns>
        [HttpGet]
        [Route("api/owners/{ownerId}/pets")]
        [ResponseType(typeof(Pet[]))]
        public IHttpActionResult GetPets(int ownerId)
        {
            try
            {
                var pets = context.Pets.Where(p => p.idOwner == ownerId);
                if (pets == null)
                    return NotFound();

                return Ok(pets);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        private Owner PopulateVetFromRM(OwnerRM owner)
        {
            return new Owner()
            {
                firstname = owner.Firstname,
                lastname = owner.Lastname,
                birthday = owner.Birthday,
                phone = owner.Phone
            };
        }
    }
}
