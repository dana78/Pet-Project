using PetApiClient;
using PetApiClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PetProject.Web.Controllers
{
    public class ClinicHistoryController : Controller
    {
        // GET: ClinicHistory
        public ActionResult Create(int? petId)
        {
            Vet vet = (Session["Vet"] as Vet);
            if (vet == null)
                return RedirectToAction("Login", "User");

            var history = new ClinicHistoryRM()
            {
                PetId = petId.Value,
                VetId = vet.IdVet,
                ArrivalDate = DateTime.Now,
                DischargeDate = DateTime.Now
            };
            return View(history);
        }

        [HttpPost]
        public async Task<ActionResult> Save(ClinicHistoryRM history)
        {
            var appId = Session["AppId"] as int?;
            if (appId == null || !appId.HasValue)//TODO: Solve it smarter
                throw new Exception("Hubo un error");

            var postedHistory = await PetApi.Instance.PostClinicHistory(history);
            await PetApi.Instance.AttendAppointment(appId.Value);
            Session.Remove("AppId");

            return RedirectToAction("Index", "Vet");
        }
    }
}