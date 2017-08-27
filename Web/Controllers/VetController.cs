using PetApiClient;
using PetApiClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PetProject.Web.Controllers
{
    public class VetController : Controller
    {
        // GET: Vet
        public async Task<ActionResult> Index()
        {
            var vet = Session["Vet"] as Vet;
            if (vet == null)
                return RedirectToAction("Login", "User");

            var allAppointments = await PetApi.Instance.GetVetAppointments(vet.IdVet.Value);
            vet.Appointments = allAppointments.Where(a => a.Confirmed == true);
            return View(vet);
        }

        public async Task<ActionResult> Confirm(int? id)
        {
            if (id == null)
            {
                ViewBag.Message = "";
            }
            else
            {
                await PetApi.Instance.ConfirmAppointment(id.Value);
            }
            return RedirectToAction("Index", "Vet");
        }

        public async Task<ActionResult> Notifications()
        {
            var vet = Session["Vet"] as Vet;
            if (vet == null)
                return RedirectToAction("Login", "User");

            var appointments = await PetApi.Instance.GetVetAppointments(vet.IdVet.Value);
            var filtered = appointments?.Where(a => a.Confirmed == false);

            return View(filtered);
        }


        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "User");
        }
    }
}