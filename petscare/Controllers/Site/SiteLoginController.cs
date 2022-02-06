using petscare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;

namespace petscare.Controllers.Site
{
    public class SiteLoginController : Controller
    {
        public petscareEntities Entities = new petscareEntities();
        // GET: AdminLogin
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(user login)
        {
            if (ModelState.IsValid)
            {
                using (petscareEntities entities = new petscareEntities())
                {
                    //var user = entities.user.Where(x => x.user_email == login.user_email).FirstOrDefault();
                    if (entities.user.Where(x => x.user_email == login.user_email).FirstOrDefault() != null)
                    {

                        Session["userID"] = entities.user.Single(x => x.user_email == login.user_email).user_ID.ToString();
                        Session["Name"] = entities.user.Single(x => x.user_email == login.user_email).user_firstname.ToString();
                        Session["email"] = entities.user.Single(x => x.user_email == login.user_email).user_email.ToString();
                        Session["password"] = entities.user.Single(x => x.user_email == login.user_email).user_firstpassword.ToString();
                        
                    }
                    else
                    {
                        Session["error"] = "Hatalı Kullanıcı Adı yada Şifre";
                        Response.Redirect("/SiteLogin/Login");
                        return View();
                    }
                }
            }
            if (Session["Name"] != null)
            {
                Response.Redirect("/Site/Index");
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
            Response.Redirect("~/Site/Index");
            return View();
        }

    }
}
