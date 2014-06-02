
using System.Web.Optimization;
using FluentNHibernate.Utils;

namespace FoodOrder
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/script/jquery").
            //    Include("~/scripts/jqeury-2.1.1.js"));
            
            //bundles.Add(new ScriptBundle("~/script/jqueryui").
            //    Include("~/scripts/jquery-ui-1.10.4.js"));
            //    //"~/scripts/jquery.unobtrusive-ajax.js"
            //    //"~/scripts/jquery.validate.js",
            //    //"~/scripts/jquery.validate.unobtrusive-ajax.js"

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //    "~/Scripts/jquery.validate*"));

            //bundles.Add(new ScriptBundle("~/script/bootstrap").Include("~/scripts/bootstrap.js"));
            //bundles.Add(new ScriptBundle("~/script/modernizr").Include("~/scripts/modernizr-*"));
            //bundles.Add(new StyleBundle("~/content/css").Include("~/content/bootstrap.css", "~/content/site.css"));

            //BundleTable.EnableOptimizations = true;

            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/jquery-{version}.js")
                .Include("~/Scripts/jquery.unobtrusive-ajax.js")
                .Include("~/Scripts/jquery-migrate*")
                );

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/scripts/jquery-ui-1.10.4.js"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                        "~/scripts/knockout-3.1.0.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-fileupload")
                .Include("~/scripts/jQuery.FileUpload/jquery.fileupload.js")
                );

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/themes/base/jquery.ui.all.css",
                      "~/Content/jQuery.FileUpload/css/jquery.fileupload.css"
                      ));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;

        }
    }
}