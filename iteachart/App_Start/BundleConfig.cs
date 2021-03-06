﻿using System.Web;
using System.Web.Optimization;

namespace iteachart
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                "~/Scripts/knockout-{version}.js",
                "~/Scripts/common.js"));

            bundles.Add(new ScriptBundle("~/bundles/highlight").Include(
                "~/Scripts/highlight.pack.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            
            bundles.Add(new ScriptBundle("~/bundles/select2").Include(
                        "~/Scripts/select2.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/tooltipster").Include(
                        "~/Scripts/jquery.tooltipster.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/css/select2.css",
                      "~/Content/Tooltipster/css/tooltipster.css",
                      "~/Content/site.css",
                      "~/Content/Highlight/default.css"));

            bundles.Add(new ScriptBundle("~/bundles/test").Include(
                      "~/Scripts/viewmodels/testViewModel.js"));
        }
    }
}
