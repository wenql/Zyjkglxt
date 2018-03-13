using System.Web.Optimization;

namespace TcmHMS.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            //登录页
            bundles.Add(
                new StyleBundle("~/Bundles/account-vendor/css")
                    .Include("~/fonts/roboto/roboto.css", new CssRewriteUrlTransform())
                    .Include("~/fonts/material-icons/materialicons.css", new CssRewriteUrlTransform())
                    .Include("~/fonts/font-awesome.css", new CssRewriteUrlTransform())
                    .Include("~/lib/bootstrap/css/bootstrap.css", new CssRewriteUrlTransform())
                    .Include("~/lib/toastr/toastr.css", new CssRewriteUrlTransform())
                    .Include("~/lib/Waves/dist/waves.css", new CssRewriteUrlTransform())
                    .Include("~/lib/animate.css/animate.css", new CssRewriteUrlTransform())
                    .Include("~/Content/materialize.css", new CssRewriteUrlTransform())
                    .Include("~/Views/Account/_Layout.css", new CssRewriteUrlTransform())
            );

            bundles.Add(
                new ScriptBundle("~/Bundles/account-vendor/js/bottom")
                    .Include(
                        "~/lib/json2/json2.js",
                        "~/lib/jquery/dist/jquery.js",
                        "~/lib/bootstrap/js/bootstrap.js",
                        "~/lib/moment/min/moment-with-locales.js",
                        "~/lib/jquery-validation/dist/jquery.validate.js",
                        "~/lib/blockUI/jquery.blockUI.js",
                        "~/lib/toastr/toastr.js",
                        "~/lib/layer/layer.js",
                        "~/lib/spin.js/spin.js",
                        "~/lib/spin.js/jquery.spin.js",
                        "~/lib/Waves/dist/waves.js",
                        "~/Abp/Framework/scripts/abp.js",
                        "~/Abp/Framework/scripts/libs/abp.jquery.js",
                        "~/Abp/Framework/scripts/libs/abp.toastr.js",
                        "~/Abp/Framework/scripts/libs/abp.blockUI.js",
                        "~/Abp/Framework/scripts/libs/abp.spin.js",
                        "~/Abp/Framework/scripts/libs/abp.sweet-alert.js"
                    )
            );

        }
    }
}