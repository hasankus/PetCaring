using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using petscare.Models;

namespace petscare.Controllers
{
    public class AdminSignUpController : Controller
    {
        public petscareEntities entities = new petscareEntities();
        // GET: AdminSignUp
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(admin admin)
        {
            using (petscareEntities entities = new petscareEntities())
            {
                
                    if (admin.admin_firstpassword ==admin.admin_againpassword)
                    {
                        entities.admin.Add(admin);
                        entities.SaveChanges();
                        Session["AdminName"] = admin.admin_firstname;
                        Session["AdminsuccessSignUp"] = "Başarıyla üye oldunuz";
                        Response.Redirect("/AdminLogin/Login");
                    }
                    else
                    {
                        Session["errorpassword"] = "Şifreleri aynı girdiğinizden emin olun.";
                        return View();
                    }
                   
                
                

            }
            
            return View();
        }
    }
}