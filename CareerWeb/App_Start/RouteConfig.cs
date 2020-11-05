using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CareerWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "InterviewLetter",
                url: "InterviewLetter",
                defaults: new { controller = "Manage", action = "InterviewLetter", id = UrlParameter.Optional },
                namespaces: new[] { "CareerWeb.Controllers" }
            );

            routes.MapRoute(
                 name: "InviteWork",
                 url: "InviteWork",
                 defaults: new { controller = "Manage", action = "InviteWork", id = UrlParameter.Optional },
                 namespaces: new[] { "CareerWeb.Controllers" }
             );

            routes.MapRoute(
                 name: "WorkResult",
                 url: "WorkResult",
                 defaults: new { controller = "Manage", action = "Result", id = UrlParameter.Optional },
                 namespaces: new[] { "CareerWeb.Controllers" }
             );
          

            routes.MapRoute(
                name: "Tim-kiem-ung-vien",
                url: "SearchCandidate",
                defaults: new { controller = "Employee", action = "SearchCandidate", id = UrlParameter.Optional },
                namespaces : new[]{ "CareerWeb.Controllers"}
            );

            routes.MapRoute(
                name: "Dang-tin-tuyen-dung",
                url: "ListAndCreateOffer",
                defaults: new { controller = "Employee", action = "ListAndCreateOffer", id = UrlParameter.Optional },
                namespaces: new[] { "CareerWeb.Controllers" }
            );

            routes.MapRoute(
                name: "Result",
                url: "SearchCandidateResult",
                defaults: new { controller = "Employee", action = "SearchCandidateResult", id = UrlParameter.Optional },
                namespaces: new[] { "CareerWeb.Controllers" }
            );

            routes.MapRoute(
               name: "CV Template",
               url: "SearchTemplate/{template}",
               defaults: new { controller = "User", action = "Check", id = UrlParameter.Optional },
               namespaces: new[] { "CareerWeb.Controllers" }
            );

            routes.MapRoute(
                 name: "ResultForSearchJob",
                 url: "OfferDetail/{OfferID}",
                 defaults: new { controller = "User", action = "ResultForSearchJob", id = UrlParameter.Optional },
                 namespaces: new[] { "CareerWeb.Controllers" }
            );

            routes.MapRoute(
                 name: "ResultForSearchCompany",
                 url: "ResultForSearchCompany/{EnterpriseID}",
                 defaults: new { controller = "User", action = "ResultForSearchCompany", id = UrlParameter.Optional },
                 namespaces: new[] { "CareerWeb.Controllers" }
             );

            routes.MapRoute(
               name: "Search_Company_For_User",
               url: "SearchCompanyForUser",
               defaults: new { controller = "User", action = "SearchCompanyForUser", id = UrlParameter.Optional },
               namespaces: new[] { "CareerWeb.Controllers" }
          );
            routes.MapRoute(
               name: "Tim-kiem-viec-lam",
               url: "SearchJobForUser",
               defaults: new { controller = "User", action = "SearchJobForUser", id = UrlParameter.Optional },
               namespaces: new[] { "CareerWeb.Controllers" }
          );

            routes.MapRoute(
               name: "ShowDetailCandidate",
               url: "ShowDetailCandidate/{UserId}",
               defaults: new { controller = "Employee", action = "ShowDetailCandidate", id = UrlParameter.Optional },
               namespaces: new[] { "CareerWeb.Controllers" }
           );
            routes.MapRoute(
              name: "Default",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
              namespaces: new[] { "CareerWeb.Controllers" }
          );
        }
    }
}
