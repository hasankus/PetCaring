using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace petscare.AppSettings
{
    public class Security : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(HttpContext.Current.Session["userID"]== null)
            {
                filterContext.Result= new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Site",
                    action = "Index"
                }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}