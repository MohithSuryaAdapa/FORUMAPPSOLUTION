using System.Web.Optimization;

namespace UIFORUMAPP.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/Custom").Include("~/Scripts/myscript.js"));
           
            bundles.Add(new ScriptBundle("~/Scripts/bootstrap").Include("~/Scripts/jquery-3.6.3.js", "~/Scripts/umd/popper.js", "~/Scripts/bootstrap.js"));
            bundles.Add(new StyleBundle("~/Styles/bootstrap").Include("~/Content/bootstrap.min.css"));
            bundles.Add(new StyleBundle("~/Styles/MyStyles").Include("~/Content/myStyles.css"));

            BundleTable.EnableOptimizations = true;

        }
    }
}