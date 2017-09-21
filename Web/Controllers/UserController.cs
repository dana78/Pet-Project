using PetApiClient;
using PetApiClient.Services;
using PetProject.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PetProject.Web.Controllers
{
    public class UserController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(Credentials credentials)
        {
            try
            {
                var result = await PetApi.Instance.Login(credentials);
                Session["Vet"] = result.Vet;
                Session["Clinic"] = result.Clinic;
                Session["Owner"] = result.Owner;
                return RedirectToAction("Index", "Vet");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(VetRegister vetRegister)
        {
            int userId = await PetApi.Instance.RegisterNewUser(new Credentials(vetRegister.Email, vetRegister.Password));
            VetRM castedVet = vetRegister;
            castedVet.UserId = userId;
            await PetApi.Instance.PostVet(castedVet);

            return RedirectToAction("Login");
        }
    }
}