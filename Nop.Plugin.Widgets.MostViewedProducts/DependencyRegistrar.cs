using Autofac;
using Autofac.Core;
using Nop.Core.Configuration;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Web.Framework.Mvc;
using Nop.Plugin.Widgets.MostViewedProducts.Services;
using Nop.Plugin.Widgets.MostViewedProducts.Models;
using System.Web.Mvc;

namespace Nop.Plugin.Widgets.MostViewedProducts
{
    /// <summary>
    /// Dependency registrar
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="builder">Container builder</param>
        /// <param name="typeFinder">Type finder</param>
        /// <param name="config">Config</param>
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            builder.RegisterType<MVPService>().As<IMVPService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductReportService>().As<IProductReportService>().InstancePerLifetimeScope();
            builder.RegisterType<MVPActionFilter>().As<IFilterProvider>().SingleInstance();
            //data context
            this.RegisterPluginDataContext<MVPObjectContext>(builder, "nop_object_context_mvp");

            //override required repository with our custom context
            builder.RegisterType<EfRepository<MostViewedProduct>>()
                .As<IRepository<MostViewedProduct>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("nop_object_context_mvp"))
                .InstancePerLifetimeScope();
        }

        /// <summary>
        /// Order of this dependency registrar implementation
        /// </summary>
        public int Order
        {
            get { return 1; }
        }
    }
}
