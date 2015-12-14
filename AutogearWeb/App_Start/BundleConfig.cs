using System.Web.Optimization;

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
                      "~/Scripts/respond.js",
                      "~/assets/js/date-time/bootstrap-datepicker.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css"));
            bundles.Add(new StyleBundle("~/Content/AceCss").Include(
                      "~/assets/css/font-awesome.css",
                      "~/assets/css/jquery-ui.css",
                      "~/assets/css/bootstrap-datepicker3.css",
                      "~/assets/css/ui.jqgrid.css" ,
                      "~/assets/css/ace-fonts.css",
                      "~/assets/css/ace.css","~/assets/css/fullcalendar.css"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-2.1.4.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryUI").Include(
    "~/Scripts/jquery-ui-1.11.4.js"
    ));
            bundles.Add(new ScriptBundle("~/bundles/jqGrid").Include(
                "~/assets/js/jqGrid/jquery.jqGrid.js",
                "~/assets/js/jqGrid/i18n/grid.locale-en.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/Fullcalendar").Include(
                "~/assets/js/bootstrap.js",
                "~/assets/js/date-time/moment.js",
                "~/assets/js/fullcalendar.js"
                ));
            // Project specific scripts and style sheets
  //          bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
  //                      "~/Scripts/jquery-1.10.2.js",
  //                      "~/assets/js/bootstrap.js",
  //                      "~/assets/js/date-time/bootstrap-datepicker.js",
  //                      "~/assets/js/jqGrid/jquery.jqGrid.js",
  //                      "~/assets/js/jqGrid/i18n/grid.locale-en.js",
  //                      "~/Scripts/jquery-ui.js",
  //                      "~/Scripts/jquery-ui.custom.js",
  //                        "~/assets/js/jquery-ui.custom.js",
  //  "~/assets/js/jquery.ui.touch-punch.js",
  //  "~/assets/js/jquery.easypiechart.js",
  //  "~/assets/js/jquery.sparkline.js"
  //));

            // Theme js
    //        bundles.Add(new ScriptBundle("~/bundles/ace").Include(
    //                      "~/assets/js/ace/elements.scroller.js",
    //"~/assets/js/ace/elements.colorpicker.js",
    //"~/assets/js/ace/elements.fileinput.js",
    //"~/assets/js/ace/elements.typeahead.js",
    //"~/assets/js/ace/elements.wysiwyg.js",
    //"~/assets/js/ace/elements.spinner.js",
    //"~/assets/js/ace/elements.treeview.js",
    //"~/assets/js/ace/elements.wizard.js",
    //"~/assets/js/ace/elements.aside.js",
    //"~/assets/js/ace/ace.js",
    //"~/assets/js/ace/ace.ajax-content.js",
    //"~/assets/js/ace/ace.touch-drag.js",
    //"~/assets/js/ace/ace.sidebar.js",
    //"~/assets/js/ace/ace.sidebar-scroll-1.js",
    //"~/assets/js/ace/ace.submenu-hover.js",
    //"~/assets/js/ace/ace.widget-box.js",
    //"~/assets/js/ace/ace.settings.js",
    //"~/assets/js/ace/ace.settings-rtl.js",
    //"~/assets/js/ace/ace.settings-skin.js",
    //"~/assets/js/ace/ace.widget-on-reload.js",
    //"~/assets/js/ace/ace.searchbox-autocomplete.js",
    //                    "~/assets/autogear/js/index.js",
    //                    "~/assets/autogear/js/instructor.js"));
           

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
