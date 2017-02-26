using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Nop.Core.Infrastructure;
using Nop.Plugin.Widgets.MostViewedProducts.Controllers;

using Nop.Plugin.Widgets.MostViewedProducts.Services;
using Nop.Services.Catalog;


namespace Nop.Plugin.Widgets.MostViewedProducts
{
    public class MVPActionFilter : ActionFilterAttribute, IFilterProvider, IActionFilter
    {
        private readonly IMVPService _mvpService;
        private readonly IProductService _productService;

        public MVPActionFilter(IMVPService mvpService,
            IProductService productService
            )
        {
            this._productService = productService;
            this._mvpService = mvpService;
        }

        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            if (controllerContext.Controller is Nop.Web.Controllers.ProductController && actionDescriptor.ActionName.Equals("ProductDetails", StringComparison.InvariantCultureIgnoreCase))
            {
                return new List<Filter>() { new Filter(this, FilterScope.Action, 0) };
            }

            return new List<Filter>();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var routeValues = new RouteValueDictionary();
            var productId = int.Parse(filterContext.RouteData.Values["productId"].ToString());

            var product = _productService.GetProductById(productId);
            //Update product view count
            _mvpService.Log(product);

            //var mvpController = EngineContext.Current.Resolve<MVPController>(); // my plugin controller
            //filterContext.Result = mvpController.UpdateViewCount(productId); // my controller method
        }
    }
}
