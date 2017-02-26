using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Seo;
using Nop.Core.Domain.Vendors;
using Nop.Core.Infrastructure;
using Nop.Services.Catalog;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Events;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Shipping;
using Nop.Services.Stores;
using Nop.Services.Tax;
using Nop.Services.Vendors;
using Nop.Services.Configuration;
using Nop.Web.Extensions;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Security;
using Nop.Web.Framework.Security.Captcha;
using Nop.Web.Infrastructure.Cache;
using Nop.Web.Models.Catalog;
using Nop.Web.Models.Common;
using Nop.Web.Models.Media;
using Nop.Plugin.Widgets.MostViewedProducts.Models;
using Nop.Web.Controllers;
using Nop.Plugin.Widgets.MostViewedProducts.Services;
using System.Web.Routing;

namespace Nop.Plugin.Widgets.MostViewedProducts.Controllers
{
    public class MVPController : BasePluginController
    {
        private readonly ICategoryService _categoryService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IProductService _productService;
        private readonly IVendorService _vendorService;
        private readonly IProductTemplateService _productTemplateService;
        private readonly IProductAttributeService _productAttributeService;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly ITaxService _taxService;
        private readonly ICurrencyService _currencyService;
        private readonly IPictureService _pictureService;
        private readonly ILocalizationService _localizationService;
        private readonly IMeasureService _measureService;
        private readonly IPriceCalculationService _priceCalculationService;
        private readonly IPriceFormatter _priceFormatter;
        private readonly IWebHelper _webHelper;
        private readonly ISpecificationAttributeService _specificationAttributeService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IRecentlyViewedProductsService _recentlyViewedProductsService;
        private readonly ICompareProductsService _compareProductsService;
        private readonly IWorkflowMessageService _workflowMessageService;
        private readonly IProductTagService _productTagService;
        private readonly IOrderReportService _orderReportService;
        private readonly IAclService _aclService;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IPermissionService _permissionService;
        private readonly IDownloadService _downloadService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IProductAttributeParser _productAttributeParser;
        private readonly IShippingService _shippingService;
        private readonly IEventPublisher _eventPublisher;
        private readonly MediaSettings _mediaSettings;
        private readonly CatalogSettings _catalogSettings;
        private readonly VendorSettings _vendorSettings;
        private readonly ShoppingCartSettings _shoppingCartSettings;
        private readonly LocalizationSettings _localizationSettings;
        private readonly CustomerSettings _customerSettings;
        private readonly CaptchaSettings _captchaSettings;
        private readonly SeoSettings _seoSettings;
        private readonly ICacheManager _cacheManager;

        private readonly IStoreService _storeService;
        private readonly ISettingService _settingService;
        private readonly IProductReportService _productReportService;
        private readonly IMVPService _mvpService;

        public MVPController(ICategoryService categoryService,
            IManufacturerService manufacturerService,
            IProductService productService,
            IVendorService vendorService,
            IProductTemplateService productTemplateService,
            IProductAttributeService productAttributeService,
            IWorkContext workContext,
            IStoreContext storeContext,
            ITaxService taxService,
            ICurrencyService currencyService,
            IPictureService pictureService,
            ILocalizationService localizationService,
            IMeasureService measureService,
            IPriceCalculationService priceCalculationService,
            IPriceFormatter priceFormatter,
            IWebHelper webHelper,
            ISpecificationAttributeService specificationAttributeService,
            IDateTimeHelper dateTimeHelper,
            IRecentlyViewedProductsService recentlyViewedProductsService,
            ICompareProductsService compareProductsService,
            IWorkflowMessageService workflowMessageService,
            IProductTagService productTagService,
            IOrderReportService orderReportService,
            IAclService aclService,
            IStoreMappingService storeMappingService,
            IPermissionService permissionService,
            IDownloadService downloadService,
            ICustomerActivityService customerActivityService,
            IProductAttributeParser productAttributeParser,
            IShippingService shippingService,
            IEventPublisher eventPublisher,
            MediaSettings mediaSettings,
            CatalogSettings catalogSettings,
            VendorSettings vendorSettings,
            ShoppingCartSettings shoppingCartSettings,
            LocalizationSettings localizationSettings,
            CustomerSettings customerSettings,
            CaptchaSettings captchaSettings,
            SeoSettings seoSettings,
            ICacheManager cacheManager,
            IStoreService storeService,
            ISettingService settingService,
            IProductReportService productReportService,
            IMVPService mvpService

            )
        {
            this._categoryService = categoryService;
            this._manufacturerService = manufacturerService;
            this._productService = productService;
            this._vendorService = vendorService;
            this._productTemplateService = productTemplateService;
            this._productAttributeService = productAttributeService;
            this._workContext = workContext;
            this._storeContext = storeContext;
            this._taxService = taxService;
            this._currencyService = currencyService;
            this._pictureService = pictureService;
            this._localizationService = localizationService;
            this._measureService = measureService;
            this._priceCalculationService = priceCalculationService;
            this._priceFormatter = priceFormatter;
            this._webHelper = webHelper;
            this._specificationAttributeService = specificationAttributeService;
            this._dateTimeHelper = dateTimeHelper;
            this._recentlyViewedProductsService = recentlyViewedProductsService;
            this._compareProductsService = compareProductsService;
            this._workflowMessageService = workflowMessageService;
            this._productTagService = productTagService;
            this._orderReportService = orderReportService;
            this._aclService = aclService;
            this._storeMappingService = storeMappingService;
            this._permissionService = permissionService;
            this._downloadService = downloadService;
            this._customerActivityService = customerActivityService;
            this._productAttributeParser = productAttributeParser;
            this._shippingService = shippingService;
            this._eventPublisher = eventPublisher;
            this._mediaSettings = mediaSettings;
            this._catalogSettings = catalogSettings;
            this._vendorSettings = vendorSettings;
            this._shoppingCartSettings = shoppingCartSettings;
            this._localizationSettings = localizationSettings;
            this._customerSettings = customerSettings;
            this._captchaSettings = captchaSettings;
            this._seoSettings = seoSettings;
            this._cacheManager = cacheManager;
            this._storeService = storeService;
            this._settingService = settingService;
            this._productReportService = productReportService;
            this._mvpService = mvpService;
        }
        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure()
        {
            var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            var mvpSettings = _settingService.LoadSetting<MVPSettings>(storeScope);
            var model = new MVPSettingsModel();
            model.ShowMVPOnHomepage = mvpSettings.ShowMVPOnHomepage;
            model.NumberOfMVPOnHomepage = mvpSettings.NumberOfMVPOnHomepage;

            model.ActiveStoreScopeConfiguration = storeScope;
            if (storeScope > 0)
            {
                model.ShowMVPOnHomepage_OverrideForStore = _settingService.SettingExists(mvpSettings, x => x.ShowMVPOnHomepage, storeScope);
                model.NumberOfMVPOnHomepage_OverrideForStore = _settingService.SettingExists(mvpSettings, x => x.NumberOfMVPOnHomepage, storeScope);
            }
            return View("~/Plugins/Widgets.MostViewedProducts/Views/Configure/Configure.cshtml", model);
        }

        [AdminAuthorize]
        [ChildActionOnly]
        [HttpPost]
        public ActionResult Configure(MVPSettingsModel model)
        {
            //load settings for a chosen store scope
            var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            var mvpSettings = _settingService.LoadSetting<MVPSettings>(storeScope);
            mvpSettings.ShowMVPOnHomepage = model.ShowMVPOnHomepage;
            mvpSettings.NumberOfMVPOnHomepage = model.NumberOfMVPOnHomepage;

            _settingService.SaveSettingOverridablePerStore(mvpSettings, x => x.ShowMVPOnHomepage, model.ShowMVPOnHomepage_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(mvpSettings, x => x.NumberOfMVPOnHomepage, model.NumberOfMVPOnHomepage_OverrideForStore, storeScope, false);

            //now clear settings cache
            _settingService.ClearCache();

            SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));
            return Configure();
        }

        public ActionResult PublicInfo(string widgetZone, object additionalData = null)
        {
            var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            var catalogSettings = _settingService.LoadSetting<MVPSettings>(storeScope);


            if (!catalogSettings.ShowMVPOnHomepage || catalogSettings.NumberOfMVPOnHomepage == 0)
                return Content("");

            //load and cache report
            var report = _productReportService.MostViewedProducts(
                    pageSize: catalogSettings.NumberOfMVPOnHomepage)
                    .ToList();


            //load products
            var products = _productService.GetProductsByIds(report.Select(x => x.ProductId).ToArray());
            //ACL and store mapping
            products = products.Where(p => _aclService.Authorize(p) && _storeMappingService.Authorize(p)).ToList();
            //availability dates
            products = products.Where(p => p.IsAvailable()).ToList();

            if (!products.Any())
                return Content("");

            //prepare model
            var model = PrepareProductOverviewModels(products, true, true, null).ToList();
            //return PartialView(model);
            return View("~/Plugins/Widgets.MostViewedProducts/Views/PublicInfo/PublicInfo.cshtml", model);
        }

        [NonAction]
        protected virtual IEnumerable<ProductOverviewModel> PrepareProductOverviewModels(IEnumerable<Product> products,
           bool preparePriceModel = true, bool preparePictureModel = true,
           int? productThumbPictureSize = null, bool prepareSpecificationAttributes = false,
           bool forceRedirectionAfterAddingToCart = false)
        {
            return this.PrepareProductOverviewModels(_workContext,
                _storeContext, _categoryService, _productService, _specificationAttributeService,
                _priceCalculationService, _priceFormatter, _permissionService,
                _localizationService, _taxService, _currencyService,
                _pictureService, _measureService, _webHelper, _cacheManager,
                _catalogSettings, _mediaSettings, products,
                preparePriceModel, preparePictureModel,
                productThumbPictureSize, prepareSpecificationAttributes,
                forceRedirectionAfterAddingToCart);
        }
        
    }
}
