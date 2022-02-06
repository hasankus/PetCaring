using petscare.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace petscare.Controllers.UserPets
{
    public class UserPetsController : Controller
    {
        public petscareEntities entities = new petscareEntities();

        public PartialViewResult userPetsList(int userID)
        {
            user_pets userpets = entities.user_pets.FirstOrDefault(x => x.user_ID == userID);
            if (userpets != null)
            {
                List<user_petsfeatures> userpetfeatures = entities.user_petsfeatures.Where(x => x.pet_ID == userpets.pet_ID).ToList();
            }
         
            return PartialView(entities.user_pets.Where(x=>x.user_ID==userID).ToList());
        }

        public PartialViewResult userPetsAdd()
        {
            ViewBag.Vets = entities.vets.ToList();
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult userPetsAdd(int userID, int vetsID, user_pets userpets)
        {
            userpets.user_ID = entities.user.FirstOrDefault(x => x.user_ID == userID).user_ID;
            userpets.vets_ID = entities.vets.FirstOrDefault(x => x.vets_ID == vetsID).vets_ID;
            entities.user_pets.Add(userpets);
            entities.SaveChanges();
            Response.Redirect("/Site/Index");
            return PartialView("Index");
        }

        public ActionResult userPetsFeaturesAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult userPetsFeaturesAdd(int petID,int userId ,user_petsfeatures userpetsfeatures)
        {
            userpetsfeatures.pet_ID = entities.user_pets.FirstOrDefault(x => x.pet_ID == petID).pet_ID;
            entities.user_petsfeatures.Add(userpetsfeatures);
            entities.SaveChanges();
            Response.Redirect("/UserPets/userPetsList?userId="+ userId);
            return View();
        }

        public ActionResult userPetsFeaturesEdit(int petID)
        {
            return View(entities.user_petsfeatures.Where(x => x.pet_ID == petID).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult userPetsFeaturesEdit(int petID,int userId, user_petsfeatures userpetsfeatures)
        {
            try
            {
                userpetsfeatures.pet_ID = entities.user_pets.FirstOrDefault(x => x.pet_ID == petID).pet_ID;
                entities.Entry(userpetsfeatures).State = EntityState.Modified;
                entities.SaveChanges();
                Response.Redirect("/UserPets/userPetsList?userId=" + userId);
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult userPetsPictureAdd(int petID)
        {

            return View(petID);
        }

        [HttpPost]
        public ActionResult userPetsPictureAdd(int uId, HttpPostedFileBase fileUpload)
        {
            if (fileUpload != null)
            {
                Image img = Image.FromStream(fileUpload.InputStream);
                Bitmap userpetsimage = new Bitmap(img, AppSettings.SettingsUser.UsersImageSize);

                string extension = "/Images/UserPetsImages/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);
                userpetsimage.Save(Server.MapPath(extension));
                user_petspicture userpetspictures = new user_petspicture();
                userpetspictures.pet_pictureextension = extension;
                userpetspictures.pet_ID = uId;

                entities.user_petspicture.Add(userpetspictures);
                entities.SaveChanges();

            }
            return View(uId);
        }

        public ActionResult userPetsPictureUpdate(int petID)
        {

            return View(petID);
        }

        [HttpPost]
        public ActionResult userPetsPictureUpdate(int uId, HttpPostedFileBase fileUpload)
        {
            user_petspicture userpetspicture = entities.user_petspicture.FirstOrDefault(x => x.pet_ID == uId);

            if (userpetspicture != null)
            {
                entities.user_petspicture.Remove(userpetspicture);
            }
            if (fileUpload != null)
            {
                Image img = Image.FromStream(fileUpload.InputStream);
                Bitmap userpetsimage = new Bitmap(img, AppSettings.SettingsUser.UsersImageSize);

                string extension = "/Images/UserPetsImages/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);
                userpetsimage.Save(Server.MapPath(extension));
                user_petspicture userpetspictures = new user_petspicture();
                userpetspictures.pet_pictureextension = extension;
                userpetspictures.pet_ID = uId;

                entities.user_petspicture.Add(userpetspictures);
                entities.SaveChanges();

            }
            return View(uId);
        }

        public PartialViewResult UserPetsAppointments(int petID)
        {
            return PartialView(entities.appointments.Where(x => x.pet_ID == petID).ToList());
        }

        

             public ActionResult UserPetsAppoinmentsDelete(int petID)
        {
            appointments appointment = entities.appointments.FirstOrDefault(x => x.pet_ID == petID);
            entities.appointments.Remove(appointment);
            entities.SaveChanges();
            Response.Redirect("/UserPets/UserPetsAppointments?petID=" + petID);
            return RedirectToAction("UserPetsAppointments");
        }

              public ActionResult UserPetsVaccineDays(int petID)
        {
           
            return View(entities.vaccine_calendar.Where(x => x.pet_ID == petID).ToList());
        }

    }
}