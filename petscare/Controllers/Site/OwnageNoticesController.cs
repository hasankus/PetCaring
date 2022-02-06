using petscare.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace petscare.Controllers.Site
{
    public class OwnageNoticesController : Controller
    {
        public petscareEntities entities = new petscareEntities();

        public PartialViewResult ownageNoticesFilterLikeGender()
        {
            return PartialView(entities.pets_species.ToList());
        }

        public ActionResult ownageNoticesList(int petspeciesId)
        {

            return View(entities.ownage_notice.Where(x => x.notice_petspeciesID == petspeciesId).ToList());
        }

        public PartialViewResult ownageNoticesAdd()
        {
            ViewBag.provinces = entities.provinces.ToList();
            ViewBag.species = entities.pets_species.ToList();
            ViewBag.petgender = entities.pet_gender.ToList();
            ViewBag.petspecies = entities.pets_species.ToList();
            ViewBag.petsbreed = entities.pets_breed.ToList();
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult ownageNoticesAdd(int provinces_ID, int user_ID,int pets_breedID, int petgender_ID,int pets_speciesID, ownage_notice ownagenotice)
        {
            //string fileName = Path.GetFileNameWithoutExtension(ownagenotice.ImageFile.FileName);
            //string extension = Path.GetExtension(ownagenotice.ImageFile.FileName);
            //fileName = fileName + extension;
            //ownagenotice.notice_picturesextension = "/Images/OwnageNoticeImages/" + fileName;
            //fileName = Path.Combine(Server.MapPath("/Images/OwnageNoticeImages/"), fileName);
            //ownagenotice.ImageFile.SaveAs(fileName);

            ownagenotice.notice_date = DateTime.Now;
            ownagenotice.user_ID = entities.user.FirstOrDefault(x => x.user_ID == user_ID).user_ID;
            ownagenotice.notice_province = entities.provinces.FirstOrDefault(x => x.provinces_ID == provinces_ID).provinces_name;
            ownagenotice.notice_petgender = entities.pet_gender.FirstOrDefault(x => x.petgender_ID == petgender_ID).petgender_name;
            ownagenotice.notice_pictureID = ownagenotice.user_ID;
            ownagenotice.notice_petspeciesID = entities.pets_species.FirstOrDefault(x => x.pets_speciesID == pets_speciesID).pets_speciesID;
            ownagenotice.notice_petbreedID = entities.pets_breed.FirstOrDefault(x=>x.pets_breedID == pets_breedID).pets_breedID;


            entities.ownage_notice.Add(ownagenotice);
            entities.SaveChanges();
            //Response.Redirect("/OwnageNotices/userOwnageNoticeList?user_ID="+ownagenotice.user_ID);
            //return RedirectToAction("ownageNoticesFilterLikeGender");
            Response.Redirect("/OwnageNotices/ownageNoticesPictureAdd?noticeId=" + ownagenotice.notice_ID);
            return PartialView("ownageNoticesPictureAdd");


        }

        public ActionResult userOwnageNoticeList(int user_ID)
        {
            return View(entities.ownage_notice.Where(x => x.user_ID == user_ID).ToList());
        }

        public ActionResult ownageNoticesPictureAdd(int noticeId)
        {

            return View(noticeId);
        }

        [HttpPost]
        public ActionResult ownageNoticesPictureAdd(int notice_ID, HttpPostedFileBase fileUpload)
        {
            if (fileUpload != null)
            {
                Image img = Image.FromStream(fileUpload.InputStream);
                Bitmap userimage = new Bitmap(img, AppSettings.SettingsUser.UsersImageSize);

                string extension = "/Images/OwnageNoticeImages/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);
                userimage.Save(Server.MapPath(extension));
                ownagenotices_pictures ownagenoticespic = new ownagenotices_pictures();
                ownagenoticespic.notice_picturesextension = extension;
                ownagenoticespic.notice_ID = notice_ID;
                


                entities.ownagenotices_pictures.Add(ownagenoticespic);
                entities.SaveChanges();
                Response.Redirect("/OwnageNotices/userOwnageNoticeList?user_ID="+ Session["userID"]);
            }
            
            return View(notice_ID);
        }

        public ActionResult OwnageNoticeDetail(int notice_ID)
        {
           

            ViewBag.pictures = entities.ownagenotices_pictures.Where(x=>x.notice_ID==notice_ID).ToList();
            ownage_notice ownagenotice = entities.ownage_notice.FirstOrDefault(x => x.notice_ID == notice_ID);
            //List<pets_appearance> petsAppearance = entities.pets_appearance.Where(x => x.pets_ID == pets.pets_ID).ToList();
            return View(ownagenotice);
        }

       
        public ActionResult OwnageNoticeDelete(int notice_ID)
        {
            ownage_notice ownagenotice = entities.ownage_notice.FirstOrDefault(x => x.notice_ID == notice_ID);
            ownagenotices_pictures ownagenoticepicture = entities.ownagenotices_pictures.FirstOrDefault(x => x.notice_ID == notice_ID);
            entities.ownage_notice.Remove(ownagenotice);
            if (ownagenoticepicture != null)
            {
                entities.ownagenotices_pictures.Remove(ownagenoticepicture);
            }

            entities.SaveChanges();
            Response.Redirect("/OwnageNotices/userOwnageNoticeList?user_ID=" + Session["userID"]);
            return RedirectToAction("userOwnageNoticeList");
        }


        public ActionResult OwnageNoticeEdit(int notice_ID)
        {
            return View(entities.ownage_notice.Where(x => x.notice_ID == notice_ID).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult OwnageNoticeEdit(int notice_ID, ownage_notice ownagenotice)
        {
            try
            {
                entities.Entry(ownagenotice).State = EntityState.Modified;
                entities.SaveChanges();
                Response.Redirect("/OwnageNotices/userOwnageNoticeList?user_ID=" + Session["userID"]);
                return RedirectToAction("userOwnageNoticeList");
            }
            catch
            {
                return View();
            }
        }


    }
}