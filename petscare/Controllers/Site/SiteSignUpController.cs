using petscare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace petscare.Controllers.Site
{
    public class SiteSignUpController : Controller
    {
        petscareEntities entities = new petscareEntities();
        // GET: SiteSignUp
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(user user)
        {
            using (petscareEntities entities = new petscareEntities())
            {

                if (user.user_firstpassword == user.user_againpassword)
                {
                    entities.user.Add(user);
                    entities.SaveChanges();
                   
                    Session["successSignUp"] = "Başarıyla üye oldunuz";
                    Response.Redirect("/Site/Index");
                } 
                else
                    {
                        Session["errorpassword"] = "Şifreleri aynı girdiğinizden emin olun.";
                        return View();
                    }

                }
            Session["Name"] = null;
                return View();
            }
        }
    }
