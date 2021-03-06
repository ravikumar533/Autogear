﻿using System.Web.Optimization;

namespace AutogearWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
           /* bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
            */
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/morris.css",
                      "~/Content/Site.css"));
            bundles.Add(new StyleBundle("~/Content/AceCss").Include(
                      "~/assets/css/font-awesome.css",
                     "~/assets/css/chosen.css",
                      "~/assets/css/jquery-ui.css",
                      "~/assets/css/bootstrap-datepicker3.css",
                      "~/assets/css/bootstrap-timepicker.css",
                      "~/assets/css/bootstrap-datetimepicker.css",
                      "~/assets/css/ui.jqgrid.css" ,
                      "~/assets/css/ace-fonts.css",
                      "~/assets/css/ace.css","~/assets/css/fullcalendar.css"
                      ));
            bundles.Add(new StyleBundle("~/Content/chosencss").Include(
                      "~/assets/css/chosen.css"));
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-2.1.4.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryUI").Include(
                "~/Scripts/jquery.form.js",
                "~/Scripts/jquery-ui-1.11.4.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/bootstrappicker").Include(
                "~/assets/js/date-time/bootstrap-datepicker.js",
                "~/assets/js/date-time/bootstrap-timepicker.js",
                "~/assets/js/date-time/bootstrap-datetimepicker.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/jqGrid").Include(
                "~/assets/js/jqGrid/jquery.jqGrid.js",
                "~/assets/js/jqGrid/i18n/grid.locale-en.js"
                ));
            
            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
            "~/assets/js/date-time/moment.js"    ));
            bundles.Add(new ScriptBundle("~/bundles/Fullcalendar").Include(
                "~/assets/js/fullcalendar.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/fullcalendarv231").Include(
                "~/assets/js/fullcalendarv231.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/FullcalendarColumn").Include(
                "~/assets/js/fullcalendar-columns.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/chosenjs").Include(
                "~/assets/js/chosen.jquery.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/barChart").Include(
                "~/Scripts/morris.min.js",
                "~/Scripts/raphael-min.js"
                ));
            BundleTable.EnableOptimizations = true;
        }
    }
}
