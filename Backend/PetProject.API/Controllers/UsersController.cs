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
    /// Users API
    /// </summary>
    public class UsersController : ApiController
    {
        SQLMappingDataContext context = new SQLMappingDataContext();

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>New user id</returns>
        [HttpPost]
        [Route("api/users")]
        [ResponseType(typeof(int))]
        public IHttpActionResult Post([FromBody]Credentials user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var newUser = new User()
                {
                    email = user.Email,
                    password = user.Password,
                    emailConfirmed = false
                };
                
                context.Users.InsertOnSubmit(newUser);
                context.SubmitChanges(System.Data.Linq.ConflictMode.FailOnFirstConflict);

                return Ok(newUser.idUser);
            }
            catch(Exception e)
            {
                return InternalServerError(e);
            }

        }

        /// <summary>
        /// Changes a user password
        /// </summary>
        /// <param name="id">User id</param>
        /// <param name="password">New password</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/users/{id}/change-password")]
        public IHttpActionResult ChangePassword(int id, [FromBody]string password)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var user = context.Users.FirstOrDefault(u => u.idUser == id);
                if (user == null)
                    return NotFound();

                user.password = password;
                context.SubmitChanges();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok();
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="credentials">Email and password</param>
        /// <returns>User info</returns>
        [HttpPost]
        [Route("api/users/login")]
        [ResponseType(typeof(UserRoleInfo))]
        public IHttpActionResult Login([FromBody]Credentials credentials)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var userFound = context.Users.FirstOrDefault(u => u.email == credentials.Email);

                if (userFound == null)
                    return NotFound();

                if (userFound.password != credentials.Password)
                    return NotFound();

                var userRoleInfo = new UserRoleInfo()
                {
                    Id = userFound.idUser,
                    OwnerId = userFound.idPetOwner,
                    ClinicId = userFound.idClinic,
                    VetId = userFound.idVet,
                    Owner = userFound.Owner,
                    Clinic = userFound.Clinic,
                    Vet = userFound.Vet
                };
                return Ok(userRoleInfo);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }


    }
}
