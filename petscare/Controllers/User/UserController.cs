using petscare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using System.Drawing;

namespace petscare.Controllers.User
{
    public class UserController : Controller
    {
        public petscareEntities entities = new petscareEntities();
        public PartialViewResult userAccount(int userId)
        {
            user users = entities.user.FirstOrDefault(x => x.user_ID == userId);
            List<user_pictures> userpic = entities.user_pictures.Where(x => x.user_ID == users.user_ID).ToList();
            return PartialView(entities.user.Where(x => x.user_ID == userId).ToList());
        }

        public ActionResult userAccountEdit(int id)
        {
            return View(entities.user.Where(x => x.user_ID == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult userAccountEdit(int id, user user)
        {
            try
            {
                entities.Entry(user).State = EntityState.Modified;
                entities.SaveChanges();
                Response.Redirect("/Site/Index");
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult usersPictureAdd(int userId)
        {

            return View(userId);
        }

        [HttpPost]
        public ActionResult usersPictureAdd(int uId, HttpPostedFileBase fileUpload)
        {
            if (fileUpload != null)
            {
                Image img = Image.FromStream(fileUpload.InputStream);
                Bitmap userimage = new Bitmap(img, AppSettings.SettingsUser.UsersImageSize);
                
                string extension = "/Images/UserImages/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);
                userimage.Save(Server.MapPath(extension));
                user_pictures userpictures = new user_pictures();
                userpictures.user_pictureextension= extension;
                userpictures.user_ID = uId;

                entities.user_pictures.Add(userpictures);
                entities.SaveChanges();

            }
            return View(uId);
        }

        public ActionResult userPictureUpdate(int userId)
        {

            return View(userId);
        }

        [HttpPost]
        public ActionResult userPictureUpdate(int uId, HttpPostedFileBase fileUpload)
        {
            user_pictures userpicture = entities.user_pictures.FirstOrDefault(x=>x.user_ID==uId);

            if(userpicture !=null)
            {
                entities.user_pictures.Remove(userpicture);
            }
            if (fileUpload != null)
            {
                Image img = Image.FromStream(fileUpload.InputStream);
                Bitmap userimage = new Bitmap(img, AppSettings.SettingsUser.UsersImageSize);

                string extension = "/Images/UserImages/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);
                userimage.Save(Server.MapPath(extension));
                user_pictures userpictures = new user_pictures();
                userpictures.user_pictureextension = extension;
                userpictures.user_ID = uId;

                entities.user_pictures.Add(userpictures);
                entities.SaveChanges();

            }
           
            return View(uId);
        }

        public ActionResult userAddressAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult userAddressAdd(int userID,user_adress useraddress)
        {
            
            useraddress.user_ID = entities.user.FirstOrDefault(x=>x.user_ID == userID).user_ID ;
            entities.user_adress.Add(useraddress);
            entities.SaveChanges();
            Response.Redirect("/User/userAccount?userId="+ userID);
            return View();
        }

        public ActionResult userAddressEdit(int id)
        {
            return View(entities.user_adress.Where(x => x.user_ID == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult userAddressEdit(int id, user_adress useraddress)
        {
            try
            {
                useraddress.user_ID = entities.user.FirstOrDefault(x => x.user_ID == id).user_ID;
                entities.Entry(useraddress).State = EntityState.Modified;
                entities.SaveChanges();
                Response.Redirect("/User/userAccount?userId=" + id);
                return View();
            }
            catch
            {
                return View();
            }
        }

    }
}