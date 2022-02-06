using petscare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace petscare.Controllers.UserPets
{
    public class AppointmentsController : Controller
    {
        public petscareEntities entities = new petscareEntities();

       

        public ActionResult MakeAppointments(int vetsID)
        {
            Session["vetssID"] = entities.user_pets.FirstOrDefault(x=>x.vets_ID ==vetsID).vets_ID;
            Session["vetsName"] = entities.vets.FirstOrDefault(x => x.vets_ID == vetsID).vets_firstname;
            Session["vetsSurname"]= entities.vets.FirstOrDefault(x => x.vets_ID == vetsID).vets_lastname;
         

            ViewBag.Vets = entities.vets.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult MakeAppointments(int vetsID,int petID, appointments appointment)
        {
           

            appointment.vets_ID = entities.user_pets.FirstOrDefault(x => x.vets_ID == vetsID).vets_ID;
            appointment.pet_ID = entities.user_pets.FirstOrDefault(x=>x.pet_ID == petID).pet_ID;
            entities.appointments.Add(appointment);
            entities.SaveChanges();
            Response.Redirect("/Site/Index");
            return View("Index");
        }
    }
}