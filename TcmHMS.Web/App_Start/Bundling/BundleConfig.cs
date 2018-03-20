using System.Web.Optimization;

namespace TcmHMS.Web.Bundling
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();
            AddLoginFiles(bundles);
            AddMainLibCss(bundles);
            AddMainMetronicCss(bundles);

            AddMainLibJs(bundles);

            bundles.Add(
                 new ScriptBundle("~/Bundles/Main/metronic/js")
                     .Include("~/Content/metronic/assets/vendors/base/vendors.bundle.js")
                     .Include("~/Content/metronic/assets/demo/default/base/scripts.bundle.js")
                     .ForceOrdered()
                 );
            bundles.Add(
                new StyleBundle("~/Bundles/Area/css")
                    .IncludeDirectory("~/App", "*.css", true)
                    .ForceOrdered()
                );

            bundles.Add(
                new ScriptBundle("~/Bundles/Area/js")
                    .IncludeDirectory("~/App", "*.js", true)
                    .ForceOrdered()
                );
        }

        private static void AddLoginFiles(BundleCollection bundles)
        {
            bundles.Add(
                new StyleBundle("~/Bundles/login-vendor/css")
                    .Include("~/fonts/roboto/roboto.css", new CssRewriteUrlTransform())
                    .Include("~/fonts/material-icons/materialicons.css", new CssRewriteUrlTransform())
                    .Include("~/libs/bootstrap/css/bootstrap.css", new CssRewriteUrlTransform())
                    .Include("~/libs/toastr/toastr.css", new CssRewriteUrlTransform())
                    .Include("~/libs/Waves/dist/waves.css", new CssRewriteUrlTransform())
                    .Include("~/libs/animate.css/animate.css", new CssRewriteUrlTransform())
                    .Include("~/Content/materialize.css", new CssRewriteUrlTransform())
                    .Include("~/Views/Account/_Layout.css", new CssRewriteUrlTransform())
            );
            bundles.Add(
                new ScriptBundle("~/Bundles/login-vendor/js/bottom")
                    .Include(
                        "~/libs/json2/json2.js",
                        "~/libs/jquery/jquery.js",
                        "~/libs/bootstrap/js/bootstrap.js",
                        "~/libs/moment/min/moment-with-locales.js",
                        "~/libs/jquery-validation/dist/jquery.validate.js",
                        "~/libs/blockUI/jquery.blockUI.js",
                        "~/libs/toastr/toastr.min.js",
                        "~/libs/layer/layer.js",
                        "~/libs/spinjs/spin.js",
                        "~/libs/spinjs/jquery.spin.js",
                        "~/libs/Waves/dist/waves.js",
                        "~/Abp/Framework/scripts/abp.js",
                        "~/Abp/Framework/scripts/libs/abp.jquery.js",
                        "~/Abp/Framework/scripts/libs/abp.toastr.js",
                        "~/Abp/Framework/scripts/libs/abp.blockUI.js",
                        "~/Abp/Framework/scripts/libs/abp.spin.js",
                        "~/Abp/Framework/scripts/libs/abp.sweet-alert.js",
                        "~/Abp/Framework/scripts/helpers.js"
                    )
            );
        }

        private static void AddMainLibCss(BundleCollection bundles)
        {
            bundles.Add(
                new StyleBundle("~/Bundles/Main/libs/css")
                    .Include("~/fonts/font-awesome/font-awesome.min.css", new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include("~/fonts/simple-line-icons/simple-line-icons.min.css", new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include("~/libs/bootstrap/css/bootstrap.min.css", new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include("~/libs/jquery-uniform/css/uniform.default.css", new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include("~/libs/morris/morris.css")
                    .Include("~/libs/jstree/themes/default/style.css", new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include("~/libs/toastr/toastr.min.css")
                    .Include("~/libs/angular-ui-grid/ui-grid.min.css", new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include("~/libs/bootstrap-daterangepicker/daterangepicker.css")
                    .Include("~/libs/bootstrap-select/bootstrap-select.min.css")
                    .Include("~/libs/bootstrap-switch/css/bootstrap-switch.min.css")
                    .Include("~/libs/jcrop/css/jquery.Jcrop.min.css")
                    .ForceOrdered()
                );
        }

        private static void AddMainMetronicCss(BundleCollection bundles)
        {
            bundles.Add(
                new StyleBundle("~/Bundles/Main/metronic/css")
                    .Include("~/Content/metronic/assets/vendors/base/vendors.bundle.css", new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include("~/Content/metronic/assets/demo/default/base/style.bundle.css", new CssRewriteUrlWithVirtualDirectoryTransform())
                    .ForceOrdered()
                );
        }

        private static void AddMainLibJs(BundleCollection bundles)
        {
            bundles.Add(
               new ScriptBundle("~/Bundles/Main/libs/js").Include(
                    "~/libs/json2/json2.min.js",
                    "~/libs/jquery/jquery.js",
                    "~/libs/jquery/jquery-migrate.min.js",
                    "~/libs/bootstrap/js/bootstrap.min.js",
                    "~/libs/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js",
                    "~/libs/jquery-slimscroll/jquery.slimscroll.min.js",
                    "~/libs/jquery-blockui/jquery.blockui.min.js",
                    "~/libs/js-cookie/js.cookie.min.js",
                    "~/libs/jquery-uniform/jquery.uniform.min.js",
                    "~/libs/jquery.dataTables/jquery.dataTables.js",
                    "~/libs/jquery.signalR/jquery.signalR-2.2.2.min.js",
                    "~/libs/localforage/localforage.min.js",
                    "~/libs/morris/morris.min.js",
                    "~/libs/morris/raphael-min.js",
                    "~/libs/jquery-sparkline/jquery.sparkline.min.js",
                    "~/libs/jcrop/js/jquery.color.js",
                    "~/libs/jcrop/js/jquery.Jcrop.min.js",
                    "~/libs/jquery-timeago/jquery.timeago.js",
                    "~/libs/jstree/jstree.js",
                    "~/libs/bootstrap-switch/js/bootstrap-switch.min.js",
                    "~/libs/spinjs/spin.js",
                    "~/libs/spinjs/jquery.spin.js",
                    "~/libs/layer/layer.js",
                    "~/libs/push-js/push.min.js",
                    "~/libs/toastr/toastr.min.js",
                    "~/libs/moment/moment-with-locales.min.js",
                    "~/libs/moment/moment-timezone-with-data.min.js",
                    "~/libs/bootstrap-daterangepicker/daterangepicker.js",
                    "~/libs/bootstrap-select/bootstrap-select.min.js",
                    "~/Scripts/underscore.min.js",
                    "~/Scripts/angular.js",
                    "~/Scripts/angular-sanitize.min.js",
                    "~/Scripts/angular-touch.min.js",
                    "~/Scripts/angular-ui-router.min.js",
                    "~/Scripts/angular-ui/ui-utils.min.js",
                    "~/Scripts/angular-ui/ui-bootstrap-tpls.min.js",
                    "~/libs/angular-ui-grid/ui-grid.min.js",
                    "~/libs/angular-file-upload/angular-file-upload.min.js",
                    "~/libs/angular-ocLazyLoad/ocLazyLoad.min.js",
                    "~/libs/angular-daterangepicker/angular-daterangepicker.min.js",
                    "~/libs/angular-metronic-datatables/angular-metronic-datatables.js",
                    "~/libs/angular-moment/angular-moment.min.js",
                    "~/libs/angular-bootstrap-switch/angular-bootstrap-switch.min.js",
                    "~/Abp/Framework/scripts/abp.js",
                    "~/Abp/Framework/scripts/libs/abp.jquery.js",
                    "~/Abp/Framework/scripts/libs/abp.toastr.js",
                    "~/Abp/Framework/scripts/libs/abp.blockUI.js",
                    "~/Abp/Framework/scripts/libs/abp.spin.js",
                    "~/Abp/Framework/scripts/libs/abp.sweet-alert.js",
                    "~/Abp/Framework/scripts/libs/abp.moment.js",
                    "~/Abp/Framework/scripts/libs/angularjs/abp.ng.js",
                    "~/Abp/Framework/scripts/helpers.js"
                   ).ForceOrdered()
               );
        }
    }
}