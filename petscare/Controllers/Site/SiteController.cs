using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using petscare.Models;

namespace petscare.Controllers
{
    public class SiteController : Controller
    {
        petscareEntities entities = new petscareEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Contacts()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contacts(Contact contact)
        {
            entities.Contact.Add(contact);
            entities.SaveChanges();
            return View();
        }
        public PartialViewResult SliderImages()
        {
            var image = entities.SliderImages.Where(x=>x.SliderImages_extension.Contains("SliderImages")).ToList();
            return PartialView(image);
        }

        //public ActionResult PetsList()
        //{
        //    return View(entities.pets.ToList());
        //}

        public PartialViewResult PetsSpeciesList()
        {
            return PartialView(entities.pets_species.ToList());
        }

        public PartialViewResult PetsList(int petspeciesId)
        {
            return PartialView(entities.pets.Where(x=>x.pets_speciesID==petspeciesId).ToList());
        }

        public ActionResult PetsDetail(int id)
        {
            pets pets = entities.pets.FirstOrDefault(x=>x.pets_ID==id);
            List<pets_appearance> petsAppearance = entities.pets_appearance.Where(x=>x.pets_ID ==pets.pets_ID).ToList();
            List<pets_breed> petsBreed = entities.pets_breed.Where(x => x.pets_breedID == pets.pets_breedID).ToList();
            return View(pets);
        }

        public PartialViewResult Menu()
        {
            ViewBag.user = entities.user.ToList();
            return PartialView();
        }

        public PartialViewResult Categories()
        {
            return PartialView(entities.pets_species.ToList());
        }

        public PartialViewResult OwnageNoticesList()
        {
            return PartialView(entities.ownage_notice.ToList());
        }

        public PartialViewResult VeterinaryMap()
        {
            return PartialView();
        }
    }
}