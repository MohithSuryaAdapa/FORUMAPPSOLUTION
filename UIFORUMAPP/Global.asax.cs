using System.Web.Mvc;
using System.Web.Routing;
using UIFORUMAPP.App_Start;
using System.Web.Optimization;

namespace UIFORUMAPP
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            UnityConfig.RegisterComponents();
            BundleConfig.RegisterBundles(BundleTable.Bundles);


        }
    }
}
