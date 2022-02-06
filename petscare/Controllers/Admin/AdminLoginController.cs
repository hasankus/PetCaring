using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;
using petscare.Models;
using System.Drawing;
using System.Configuration;
using System.IO;
using System.Web.Security;
using System.Data.Entity;

namespace petscare.Controllers
{
    public class AdminLoginController : Controller
    {
        public petscareEntities Entities = new petscareEntities();
        // GET: AdminLogin
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(admin login)
        {
            if (ModelState.IsValid)
            {
                using (petscareEntities entities = new petscareEntities())
                {
                    var user = entities.admin.Where(x => x.admin_email == login.admin_email).SingleOrDefault();
                    if (user != null)
                    {
                        if (user.admin_email == login.admin_email && user.admin_firstpassword == login.admin_firstpassword)
                        {
                            Session["AdminuserID"] = user.admin_ID.ToString();
                            Session["AdminName"] = user.admin_firstname.ToString();
                            Session["Adminemail"] = user.admin_email.ToString();
                            Session["Adminpassword"] = user.admin_firstpassword.ToString();

                            Response.Redirect("/Admin/Index");
                        }
                    }
                    Session["error"] = "Hatalı Kullanıcı Adı yada Şifre";
                }
            }


            return View();
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();

            FormsAuthentication.SignOut();

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now);
            Response.Redirect("~/AdminLogin/Login");
            return View();
        }

}
}