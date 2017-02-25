using Nop.Core.Domain.Catalog;
using Nop.Plugin.Widgets.MostViewedProducts.Models;
using Nop.Core.Data;
using System.Linq;

namespace Nop.Plugin.Widgets.MostViewedProducts.Services
{
    public class MVPService : IMVPService
    {
        private readonly IRepository<MostViewedProduct> _MVPRepository;

        public MVPService(IRepository<MostViewedProduct> MVPRepository)
        {
            _MVPRepository = MVPRepository;
        }

        /// <summary>
        /// Logs the specified record.
        /// </summary>
        /// <param name="record">The record.</param>
        public void Log(Product product)
        {

            var query = (from p in _MVPRepository.Table
                         where p.ProductId == product.Id
                         select p);

            if (query.Count() == 0)
            {
                MostViewedProduct model = new MostViewedProduct();
                model.ProductId = product.Id;
                model.ProductViewCount = 1;
                _MVPRepository.Insert(model);
            }
            else
            {
                var model = query.First();
                model.ProductViewCount++;
                _MVPRepository.Update(model);
            }
        }
    }
}
