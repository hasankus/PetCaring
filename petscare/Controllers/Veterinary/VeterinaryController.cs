using petscare.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using petscare.AppSettings;

namespace petscare.Controllers.Veterinary
{
    public class VeterinaryController : Controller
    {
        public petscareEntities entities = new petscareEntities();
        // GET: AdminLogin
        public ActionResult VeterinaryLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VeterinaryLogin(vets vetsLogin)
        {
            if (ModelState.IsValid)
            {
                using (petscareEntities entities = new petscareEntities())
                {
                    var vetuser = entities.vets.Where(x => x.vets_username == vetsLogin.vets_username).FirstOrDefault();
                    if (vetuser != null)
                    {
                        if (vetuser.vets_username == vetsLogin.vets_username && vetuser.vets_firstpassword == vetsLogin.vets_firstpassword)
                        {
                            Session["vetID"] = vetuser.vets_ID.ToString();
                            Session["vetName"] = vetuser.vets_firstname.ToString();
                            Session["vetemail"] = vetuser.vets_email.ToString();
                            Session["vetpassword"] = vetuser.vets_firstpassword.ToString();

                            Response.Redirect("/Site/Index");
                        }
                    }
                    Session["veterror"] = "Hatalı Kullanıcı Adı yada Şifre";
                }
            }


            return View();
        }

        public ActionResult VeterinaryLogOut()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();

            FormsAuthentication.SignOut();

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now);
            Response.Redirect("~/Site/Index");
            return View();
        }

        public ActionResult VeterinarySignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VeterinarySignUp(vets vetuser)
        {
            using (petscareEntities entities = new petscareEntities())
            {
               
                if (vetuser.vets_firstpassword == vetuser.vets_passwordagain)
                {
                        entities.vets.Add(vetuser);
                        entities.SaveChanges();
                        Session["vetName"] = vetuser.vets_firstname;
                        Session["vetsuccessSignUp"] = "Başarıyla üye oldunuz";
                        Response.Redirect("/Site/Index");
                }
                else
                {
                        Session["veterrorpassword"] = "Şifreleri aynı girdiğinizden emin olun.";
                        return View();
                }

                }
                return View();
            }

        public ActionResult VeterinaryAccount(int vetId)
        {
            vets veterinary = entities.vets.FirstOrDefault(x => x.vets_ID == vetId);
            return PartialView(entities.vets.Where(x => x.vets_ID == vetId).ToList());
        }
        //[Security]
        public ActionResult VetAccountEdit(int vetId)
        {
            return View(entities.vets.Where(x => x.vets_ID == vetId).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult VetAccountEdit(int vetId, vets vet)
        {
            try
            {
                entities.Entry(vet).State = EntityState.Modified;
                entities.SaveChanges();
                Response.Redirect("/Veterinary/VeterinaryAccount?vetId=" + vetId);
                return View();
            }
            catch
            {
                return View();
            }
        }


        public ActionResult VetPictureAdd(int vetId)
        {

            return View(vetId);
        }

        [HttpPost]
        public ActionResult VetPictureAdd(int uId, HttpPostedFileBase fileUpload)
        {
            if (fileUpload != null)
            {
                Image img = Image.FromStream(fileUpload.InputStream);
                Bitmap vetimage = new Bitmap(img, AppSettings.SettingsUser.UsersImageSize);

                string extension = "/Images/VeterinaryImages/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);
                vetimage.Save(Server.MapPath(extension));
                vets_pictures vetpictures = new vets_pictures();
                vetpictures.vets_picturesextension = extension;
                vetpictures.vets_ID = uId;

                entities.vets_pictures.Add(vetpictures);
                entities.SaveChanges();

            }
            return View(uId);
        }

        public ActionResult VetPictureUpdate(int vetId)
        {

            return View(vetId);
        }

        [HttpPost]
        public ActionResult VetPictureUpdate(int uId, HttpPostedFileBase fileUpload)
        {
            vets_pictures vetpicture = entities.vets_pictures.FirstOrDefault(x => x.vets_ID == uId);

            if (vetpicture != null)
            {
                entities.vets_pictures.Remove(vetpicture);
            }
            if (fileUpload != null)
            {
                Image img = Image.FromStream(fileUpload.InputStream);
                Bitmap vetimage = new Bitmap(img, AppSettings.SettingsUser.UsersImageSize);

                string extension = "/Images/VeterinaryImages/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);
                vetimage.Save(Server.MapPath(extension));
                vets_pictures vetpictures = new vets_pictures();
                vetpictures.vets_picturesextension = extension;
                vetpictures.vets_ID = uId;

                entities.vets_pictures.Add(vetpictures);
                entities.SaveChanges();

            }

            return View(uId);
        }

        public ActionResult VetAddressAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VetAddressAdd(int vetId, vets_adress vetaddress)
        {

            vetaddress.vets_ID = entities.vets.FirstOrDefault(x => x.vets_ID == vetId).vets_ID;
            entities.vets_adress.Add(vetaddress);
            entities.SaveChanges();
            Response.Redirect("/Veterinary/VeterinaryAccount?vetId="+vetId);
            return View();
        }

        public ActionResult VetAddressEdit(int vetId)
        {
            return View(entities.vets_adress.Where(x => x.vets_ID == vetId).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult VetAddressEdit(int vetId, vets_adress vetaddress)
        {
            try
            {
                vetaddress.vets_ID = entities.vets.FirstOrDefault(x => x.vets_ID == vetId).vets_ID;
                entities.Entry(vetaddress).State = EntityState.Modified;
                entities.SaveChanges();
                Response.Redirect("/Veterinary/VeterinaryAccount?vetId=" + vetId);
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult VeterinaryActions(int vetId)
        {
            vets veterinary = entities.vets.FirstOrDefault(x => x.vets_ID == vetId);
            return PartialView(entities.vets.Where(x => x.vets_ID == vetId).ToList());
        }

        public ActionResult VeterinaryPetsList(int vetId)
        {
            return View(entities.user_pets.Where(x=>x.vets_ID==vetId).ToList());
        }

        public ActionResult VeterinaryAppointmentList(int vetId)
        {
            //appointments appointment = entities.appointments.FirstOrDefault(x => x.vets_ID == vetId);
            
            //if (DateTime.Now.Date == appointment.date)
            //{
            //    entities.appointments.Remove(appointment);
            //    entities.SaveChanges();
            //}
            return View(entities.appointments.Where(x => x.vets_ID == vetId).ToList());
        }

        public ActionResult VeterinaryAppointmentDelete(int vetId)
        {
            return View(entities.appointments.Where(x => x.vets_ID == vetId).ToList());
        }

        public ActionResult UserPetsInformations(int petID)
        {
            return View(entities.user_pets.Where(x => x.pet_ID == petID).ToList());
        }

        public ActionResult VeterinaryVaccineDaysList(int vetId)
        {
            return View(entities.user_pets.Where(x => x.vets_ID == vetId).ToList());
        }
        
        public ActionResult UserPetVaccineAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserPetVaccineAdd(int petID, int vetsID, vaccine_calendar vaccinecalendar)
        {
            vaccinecalendar.vets_ID = entities.vets.FirstOrDefault(x => x.vets_ID == vetsID).vets_ID;
            vaccinecalendar.pet_ID = entities.user_pets.FirstOrDefault(x=>x.pet_ID == petID).pet_ID;
            entities.vaccine_calendar.Add(vaccinecalendar);
            entities.SaveChanges();

            Response.Redirect("/Veterinary/VeterinaryVaccineDaysList?vetId=" + vetsID);
            return View();
        }
        
        public ActionResult UserPetVaccineList(int petID)
        {
            return View(entities.vaccine_calendar.Where(x => x.pet_ID == petID).ToList());
        }

        public ActionResult UserPetVaccineStatus(int petID,int vaccinecalendarID)
        {
            ViewBag.VaccineStatus = entities.vaccine_status.ToList();
            return View(entities.vaccine_calendar.Where(x => x.vaccinecalendar_ID == vaccinecalendarID).FirstOrDefault());
        }
      

        [HttpPost]
        public ActionResult UserPetVaccineStatus(int petID,int vaccinecalendarID,int vaccinestatus_ID, vaccine_calendar vaccine)
        {
            try
            {
                entities.Entry(vaccine).State = EntityState.Modified;
                vaccine.vaccine_status = entities.vaccine_status.FirstOrDefault(x=>x.vaccinestatus_ID == vaccinestatus_ID).vaccinestatus;
                entities.SaveChanges();
                Response.Redirect("/Veterinary/UserPetVaccineList?petID="+petID);
                return View();
            }
            catch
            {
                return View();
            }
        }


    }
    }
