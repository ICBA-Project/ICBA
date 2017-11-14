using System.Web;
using System.Web.Optimization;

namespace ICBA.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/sidebar.css",
                      "~/Content/table.css"));

            bundles.Add(new StyleBundle("~/bundles/layoutcss").Include(
                      "~/Content/css/bootstrap.min.css",
                      "~/Content/css/bootstrap-responsive.min.css",
                      "~/Content/css/matrix-style.css",
                      "~/Content/css/matrix-media.css",
                      "~/fonts/fonts-awesome/css/font-awesome.css",
                      "~/Content/css/jquery.gritter.css",
                      "~/Content/googleapis.css",
                      "~/Content/General.css"));

            bundles.Add(new ScriptBundle("~/bundles/layoutjs").Include(
                      "~/Scripts/js/excanvas.min.js",
                      "~/Scripts/js/jquery.min.js",
                      "~/Scripts/js/jquery.ui.custom.js",
                      "~/Scripts/js/bootstrap.min.js",
                      "~/Scripts/js/jquery.flot.min.js",
                      "~/Scripts/js/jquery.flot.resize.min.js",
                      "~/Scripts/js/jquery.peity.min.js",
                      "~/Scripts/js/matrix.js",
                      "~/Scripts/js/matrix.dashboard.js",
                      "~/Scripts/js/jquery.gritter.min.js",
                      "~/Scripts/js/matrix.interface.js",
                      "~/Scripts/js/jquery.validate.js",
                      "~/Scripts/js/matrix.form_validation.js",
                      "~/Scripts/js/jquery.wizard.js",
                      "~/Scripts/js/jquery.uniform.js",
                      "~/Scripts/js/select2.min.js",
                      "~/Scripts/js/matrix.popover.js",
                      "~/Scripts/js/jquery.dataTables.min.js",
                      "~/Scripts/js/matrix.tables.js"));
        }
    }
}
