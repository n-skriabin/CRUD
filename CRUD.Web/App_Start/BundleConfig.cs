using System.Web;
using System.Web.Optimization;

namespace CRUD.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/web/styles/kendo.common.min.css",
                      "~/Content/web/styles/kendo.material.min.css",
                      "~/Content/web/styles/kendo.mobile.all.min.css",
                      "~/Content/web/styles/kendo.rtl.min.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/styles").Include(
                      "~/Content/web/styles/kendo.common.min.css",
                      "~/Content/web/styles/kendo.material.min.css",
                      "~/Content/web/styles/kendo.mobile.all.min.css",
                      "~/Content/web/styles/kendo.rtl.min.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                "~/Scripts/kendo/kendo.all.min.js",
                 "~/Scripts/app/grids/AuthorsGrid.js",
                 "~/Scripts/app/grids/BooksGrid.js",
                 "~/Scripts/app/grids/ArticlesGrid.js",
                 "~/Scripts/app/grids/JournalsGrid.js",
                 "~/Scripts/app/grids/PublishersGrid.js",
                 "~/Scripts/app/js/angular.min.js",
                 "~/Scripts/app/js/jquery-1.12.3.min.js",
                 "~/Scripts/app/js/jszip.min.js",
                 "~/Scripts/app/js/kendo.all.min.js",
                "~/Scripts/kendo/kendo.aspnetmvc.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/js").Include(
                "~/Scripts/app/js/angular.min.js",
                 "~/Scripts/app/js/jquery-1.12.3.min.js",
                 "~/Scripts/app/js/jszip.min.js",
                 "~/Scripts/app/js/kendo.all.min.js"));

            bundles.Add(new StyleBundle("~/Content/kendo/css").Include(
                "~/Content/kendo/kendo.common-bootstrap.min.css",
                
                "~/Content/kendo/kendo.bootstrap.min.css"));

            bundles.IgnoreList.Clear();
        }
    }
}
