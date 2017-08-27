using PetApiClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PetProject.Web.Controllers
{
    public class PetController : Controller
    {
        // GET: Pet
        public async Task<ActionResult> Index(int? id)
        {
            var pet = await PetApi.Instance.GetPet(id.Value);
            pet.Histories = await PetApi.Instance.GetPetClinicHistories(id.Value);
            return View(pet);
        }
    }
}