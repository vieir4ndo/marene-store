using MareneStore.Domain.Exceptions;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MareneStore.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.RegisterComponents();
        }

        protected void Application_Error()
        {
            var ex = Server.GetLastError();

            if (ex is ValidationException vex)
            {
                HttpContext.Current.Session["ToastError"] = vex.Errors.Count == 1
                    ? vex.Errors.First()
                    : string.Join(" ", vex.Errors);
            }
            else if (ex is NotFoundException)
            {
                HttpContext.Current.Session["ToastError"] = "Not found";
            }
            else if (ex is ConflictException cfx)
            {
                HttpContext.Current.Session["ToastError"] = cfx.Message;
            }
            else if (ex is UnauthorizedException)
            {
                HttpContext.Current.Session["ToastError"] = ex.Message;
            }
            else
            {
                HttpContext.Current.Session["ToastError"] = "An unexpected error occurred.";
            }

            Server.ClearError();
            // Tenta pegar rota atual
            var routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(Context));
            string controller = "Home"; // fallback
            string action = "Index"; // fallback

            if (routeData != null)
            {
                if (routeData.Values["controller"] != null)
                    controller = routeData.Values["controller"].ToString();
                if (routeData.Values["action"] != null)
                    action = routeData.Values["action"].ToString();
            }

            // Redireciona para o Index do controller que originou o erro
            Response.Redirect($"~/{controller}/Index");
        }
    }
}
