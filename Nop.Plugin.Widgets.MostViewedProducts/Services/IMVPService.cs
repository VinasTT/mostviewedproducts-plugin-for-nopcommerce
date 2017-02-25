using Nop.Core.Domain.Catalog;

namespace Nop.Plugin.Widgets.MostViewedProducts.Services
{
    public interface IMVPService
    {
        /// <summary>
        /// Logs the specified record.
        /// </summary>
        /// <param name="record">The record.</param>
        void Log(Product product);
    }
}
