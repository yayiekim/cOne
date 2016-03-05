using System.Web;
using System.Web.Optimization;

namespace ClinicOne
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/.bin/bower_components/angular/angular.min.js",
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/.bin/bower_components/bootstrap/dist/js/bootstrap.min.js",
                       "~/.bin/bower_components/metisMenu/dist/metisMenu.min.js",
                      "~/.bin/bower_components/startbootstrap-sb-admin-2/dist/js/sb-admin-2.js",
                      "~/Scripts/respond.js"));



            bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/.bin/bower_components/bootstrap/dist/css/bootstrap.min.css",
                     "~/.bin/bower_components/font-awesome/css/font-awesome.css",
                      "~/.bin/bower_components/metisMenu/dist/metisMenu.min.css",
                      "~/.bin/bower_components/startbootstrap-sb-admin-2/dist/css/sb-admin-2.css",
                      "~/Content/site.css"));
        }
    }
}
