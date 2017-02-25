using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Orders;
using Nop.Services.Helpers;
using Nop.Plugin.Widgets.MostViewedProducts.Models;

namespace Nop.Plugin.Widgets.MostViewedProducts.Services
{
    /// <summary>
    /// Product report service
    /// </summary>
    public partial class ProductReportService : IProductReportService
    {

        #region Fields

        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<MostViewedProduct> _mostViewedProductsRepository;
        private readonly IDateTimeHelper _dateTimeHelper;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="productRepository">Product repository</param>
        /// <param name="dateTimeHelper">Datetime helper</param>
        public ProductReportService(
            IRepository<Product> productRepository,
            IRepository<MostViewedProduct> mostViewedProductsRepository,
            IDateTimeHelper dateTimeHelper)
        {
            this._productRepository = productRepository;
            this._mostViewedProductsRepository = mostViewedProductsRepository;
            this._dateTimeHelper = dateTimeHelper;
        }

        #endregion

        #region Methods

        // Dev 1.01
        /// <summary>
        /// Get most viewed products
        /// </summary>
        /// <param name="vendorId">Vendor identifier; 0 to load all records</param>
        /// <param name="categoryId">Category identifier; 0 to load all records</param>
        /// <param name="manufacturerId">Manufacturer identifier; 0 to load all records</param>
        /// <param name="createdFromUtc">Order created date from (UTC); null to load all records</param>
        /// <param name="createdToUtc">Order created date to (UTC); null to load all records</param>
        /// <param name="orderBy">1 - order by quantity, 2 - order by total amount</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Result</returns>
        public virtual IPagedList<MostViewedProduct> MostViewedProducts(
            int categoryId = 0, int manufacturerId = 0, int vendorId = 0,
            DateTime? createdFromUtc = null, DateTime? createdToUtc = null,
            int orderBy = 1,
            int pageIndex = 0, int pageSize = int.MaxValue,
            bool showHidden = false)
        {

            var query1 =  from p in _mostViewedProductsRepository.Table

                          select p;


           
            IQueryable<MostViewedProduct> query2 = query1.OrderByDescending(x => x.ProductViewCount);

            var result = new PagedList<MostViewedProduct>(query2, pageIndex, pageSize);
            return result;
        }

        #endregion
    }
}
