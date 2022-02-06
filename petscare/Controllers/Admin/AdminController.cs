using petscare.AppSettings;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using petscare.Models;

namespace petscare.Controllers
{
    public class AdminController : Controller
    {
        public petscareEntities entities = new petscareEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddSliderImage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSliderImage(HttpPostedFileBase imageUpload)
        {
            if (imageUpload != null)
            {
                Image image = Image.FromStream(imageUpload.InputStream);
                Bitmap bitmap = new Bitmap(image, SettingsSliders.SliderImageSize);
                string extension = "/Images/SliderImages/" + Guid.NewGuid() + Path.GetExtension(imageUpload.FileName);
                bitmap.Save(Server.MapPath(extension));

                SliderImages sliderimages = new SliderImages();
                sliderimages.SliderImages_extension = extension;
                entities.SliderImages.Add(sliderimages);
                entities.SaveChanges();
            }
            return RedirectToAction("AddSliderImage");
        }

        public ActionResult DeleteSliderImage(int sliderID)
        {
            SliderImages sliderimages = entities.SliderImages.FirstOrDefault(x => x.SliderImages_ID == sliderID);
            entities.SliderImages.Remove(sliderimages);
            entities.SaveChanges();
            return RedirectToAction("SliderImagesList");
        }

        public ActionResult SliderImagesList()
        {
            return View(entities.SliderImages.ToList());
        }

        public ActionResult OwnageNoticesList()
        {
            return View(entities.ownage_notice.ToList());
        }

        public ActionResult UsersList()
        {
            return View(entities.user.ToList());
        }

        public ActionResult VetsList()
        {
            return View(entities.vets.ToList());
        }
        public ActionResult ContactsList()
        {
            return View(entities.Contact.ToList());
        }


    }
}