using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Catalog;

using Nop.Plugin.Widgets.MostViewedProducts.Models;

namespace Nop.Plugin.Widgets.MostViewedProducts.Services
{
    /// <summary>
    /// Product report service interface
    /// </summary>
    public partial interface IProductReportService
    {
        /// <summary>
        /// Get best sellers report
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
        IPagedList<MostViewedProduct> MostViewedProducts(
            int categoryId = 0, int manufacturerId = 0,
            int vendorId = 0,
            DateTime? createdFromUtc = null, DateTime? createdToUtc = null,
            int orderBy = 1,
            int pageIndex = 0, int pageSize = int.MaxValue,
            bool showHidden = false);
    }
}
