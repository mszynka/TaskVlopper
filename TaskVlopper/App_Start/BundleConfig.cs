using System.Web;
using System.Web.Optimization;

namespace TaskVlopper
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/styles")
                .Include(
                    "~/Content/bootstrap.css",
                      //"~/Content/bootstrap-theme.css",
                      "~/Content/bootstrap-datetimepicker.css",
                      "~/Content/jquery.modal.css",
                      "~/Content/ui-bootstrap-csp.css",
                      "~/Content/Site.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/jquery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval")
                .Include("~/Scripts/jquery/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/plugins")
                .IncludeDirectory("~/Scripts/plugins", "*.js", true)
                .Include("~/Scripts/initializers.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr")
                .Include("~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                .Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond/*.js",
                      "~/Scripts/moment/*.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/angular")
                .Include(
                "~/Scripts/angular/angular.js",
                "~/Scripts/angular/angular-animate.js",
                "~/Scripts/angular/angular-mocks.js",
                "~/Scripts/angular/angular-ui-router.js",
                "~/Scripts/angular-ui/ui-bootstrap.js",
                "~/Scripts/angular-ui/ui-bootstrap-tpls.js"
                ));
        }
    }
}
