using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nop.Plugin.Widgets.MostViewedProducts
{
    public static class GenericPathRouteExtensions
    {
        //Override for localized route
        public static Route MapGenericPathRoute1(this RouteCollection routes, string name, string url)
        {
            return MapGenericPathRoute1(routes, name, url, null /* defaults */, (object)null /* constraints */);
        }
        public static Route MapGenericPathRoute1(this RouteCollection routes, string name, string url, object defaults)
        {
            return MapGenericPathRoute1(routes, name, url, defaults, (object)null /* constraints */);
        }
        public static Route MapGenericPathRoute1(this RouteCollection routes, string name, string url, object defaults, object constraints)
        {
            return MapGenericPathRoute1(routes, name, url, defaults, constraints, null /* namespaces */);
        }
        public static Route MapGenericPathRoute1(this RouteCollection routes, string name, string url, string[] namespaces)
        {
            return MapGenericPathRoute1(routes, name, url, null /* defaults */, null /* constraints */, namespaces);
        }
        public static Route MapGenericPathRoute1(this RouteCollection routes, string name, string url, object defaults, string[] namespaces)
        {
            return MapGenericPathRoute1(routes, name, url, defaults, null /* constraints */, namespaces);
        }
        public static Route MapGenericPathRoute1(this RouteCollection routes, string name, string url, object defaults, object constraints, string[] namespaces)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }

            var route = new GenericPathRoute(url, new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(defaults),
                Constraints = new RouteValueDictionary(constraints),
                DataTokens = new RouteValueDictionary()
            };

            routes.Add(name, route);

            return route;
        }
    }
}