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
    public class AppointmentController : Controller
    {
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = await PetApi.Instance.GetAppointment(id.Value);
            if(appointment == null)
            {
                return HttpNotFound("La cita no ha sido encontrada");
            }
            return View(appointment);
        }
    }
}