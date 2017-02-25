using System.Web.Mvc;
using System.Web.Routing;
using Nop.Web.Framework.Mvc.Routes;
using Nop.Web.Framework.Seo;

namespace Nop.Plugin.Widgets.MostViewedProducts
{
    public partial class GenericUrlRouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            

            routes.MapGenericPathRoute1("Nop.Plugin.Widgets.MostViewedProducts.GenericUrl",
                                       "{generic_se_name}",
                                       new { controller = "Common", action = "GenericUrl" },
                                       new[] { "Nop.Plugin.Widgets.MostViewedProducts.Controllers" });




            //routes.Remove(routes["Nop.Plugin.Widgets.MostViewedProducts.Category"]);




        }
        public int Priority
        {
            get
            {
                return -1;
            }
        }
    }
}
