using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using petscare.AppSettings;
using petscare.Models;

namespace petscare.Controllers.Admin
{

    public class animalsController : Controller
    {
        public petscareEntities entities = new petscareEntities();



        public ActionResult petsList()
        {
            return View(entities.pets.ToList());
        }

        public ActionResult petsAdd()
        {
            ViewBag.pets_species = entities.pets_species.ToList();
            ViewBag.pets_breed = entities.pets_breed.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult petsAdd(pets pet)
        {
            

            entities.pets.Add(pet);
            entities.SaveChanges();
            return RedirectToAction("petsList");
        }

        public ActionResult petsDelete(int petsID)
        {
            pets pets = entities.pets.FirstOrDefault(x => x.pets_ID == petsID);
            entities.pets.Remove(pets);
            entities.SaveChanges();
            return RedirectToAction("petsList");
        }


        public ActionResult petSpecies()
        {
            return View(entities.pets_species.ToList());
        }

        public ActionResult petSpeciesAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult petSpeciesAdd(pets_species petspecies)
        {
            entities.pets_species.Add(petspecies);
            entities.SaveChanges();
            return RedirectToAction("petSpecies");
        }
        public ActionResult petSpeciesDelete(int petspeciesID)
        {
            pets_species petspecies = entities.pets_species.FirstOrDefault(x => x.pets_speciesID == petspeciesID);
            entities.pets_species.Remove(petspecies);
            entities.SaveChanges();
            return RedirectToAction("petSpecies");
        }

        public ActionResult petBreed()
        {
            ViewBag.pets_species = entities.pets_species.ToList();
            return View(entities.pets_breed.ToList());
        }

        public ActionResult petBreedAdd()
        {
            ViewBag.pets_species = entities.pets_species.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult petBreedAdd(pets_breed petsbreed)
        {
            string fileName = Path.GetFileNameWithoutExtension(petsbreed.ImageFile.FileName);
            string extension = Path.GetExtension(petsbreed.ImageFile.FileName);
            fileName = fileName + extension;
            petsbreed.pets_breedpictureextension = "/Images/PetsImages/" + fileName;
            fileName = Path.Combine(Server.MapPath("/Images/PetsImages/"), fileName);
            petsbreed.ImageFile.SaveAs(fileName);

            
            ViewBag.pets_species = entities.pets_species.ToList();
            entities.pets_breed.Add(petsbreed);
            entities.SaveChanges();
            return RedirectToAction("petBreed");
        }

        public ActionResult petBreedDelete(int petsbreedID)
        {
            pets_breed petsbreed = entities.pets_breed.FirstOrDefault(x => x.pets_breedID == petsbreedID);
            entities.pets_breed.Remove(petsbreed);
            entities.SaveChanges();
            return RedirectToAction("petBreed");
        }

        public ActionResult petAppearance()
        {
            return View(entities.pets_appearance.ToList());
        }

        public ActionResult petAppearanceDetail(int petsID)
        {
            return View(entities.pets_appearance.Where(x => x.pets_ID == petsID).ToList());
        }

        public ActionResult petAppearanceAdd()
        {
            ViewBag.pets = entities.pets.ToList();
            ViewBag.pets_species = entities.pets_species.ToList();
            ViewBag.pets_breed = entities.pets_breed.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult petAppearanceAdd(pets_appearance petappearance)
        {
            ViewBag.pets = entities.pets.ToList();
            ViewBag.pets_species = entities.pets_species.ToList();
            ViewBag.pets_breed = entities.pets_breed.ToList();
            entities.pets_appearance.Add(petappearance);
            entities.SaveChanges();
            return RedirectToAction("petAppearance");
        }

        public ActionResult petAppearanceDelete(int petappearanceID)
        {
            pets_appearance petappearance = entities.pets_appearance.FirstOrDefault(x=>x.pets_appearanceID==petappearanceID);
            entities.pets_appearance.Remove(petappearance);
            entities.SaveChanges();
            return RedirectToAction("petAppearance");
        }

       
      




    }
}