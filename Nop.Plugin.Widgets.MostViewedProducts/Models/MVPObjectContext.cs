using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Nop.Data;
using Nop.Core;
using System.Data.Entity.Infrastructure;

namespace Nop.Plugin.Widgets.MostViewedProducts.Models
{
    public class MVPObjectContext : DbContext, IDbContext
    {
        public MVPObjectContext(string nameOrConnectionString) : base(nameOrConnectionString) { }

        #region Implementation of IDbContext

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new MostViewedProductMap());

            base.OnModelCreating(modelBuilder);
        }

        public string CreateDatabaseInstallationScript()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateDatabaseScript();
        }

        public void Install()
        {
            //It's required to set initializer to null (for SQL Server Compact).
            //otherwise, you'll get something like "The model backing the 'your context name' context has changed since the database was created. Consider using Code First Migrations to update the database"
            Database.SetInitializer<MVPObjectContext>(null);

            Database.ExecuteSqlCommand(CreateDatabaseInstallationScript());

            var dbScript = "INSERT INTO Setting VALUES ('mvpsettings.showmvponhomepage','True',0);INSERT INTO Setting VALUES('mvpsettings.numberofmvponhomepage',4,0); ";
            Database.ExecuteSqlCommand(dbScript);

            dbScript = "INSERT INTO LocaleStringResource(LanguageId,ResourceName,ResourceValue) VALUES ('1','MVP.Settings.ShowMVPOnHomepage','Show most viewed products on homepage?');";

            dbScript += "INSERT INTO LocaleStringResource(LanguageId,ResourceName,ResourceValue) VALUES ('1','MVP.Settings.ShowMVPOnHomepage.hint','Check to show most viewed products on homepage');";
            dbScript += "INSERT INTO LocaleStringResource(LanguageId,ResourceName,ResourceValue) VALUES ('1','MVP.Settings.NumberOfMVPOnHomepage','Number of most viewed products on homepage');";
            dbScript += "INSERT INTO LocaleStringResource(LanguageId,ResourceName,ResourceValue) VALUES ('1','MVP.Settings.NumberOfMVPOnHomepage.hint','Enter the number of most viewed products you want to show on homepage');";
            Database.ExecuteSqlCommand(dbScript);
            
            SaveChanges();
        }

        public void Uninstall()
        {
            var dbScript = "DROP TABLE MostViewedProduct";
            Database.ExecuteSqlCommand(dbScript);

            dbScript = "DELETE FROM Setting where Name in ('mvpsettings.showmvponhomepage','mvpsettings.numberofmvponhomepage') ";
            Database.ExecuteSqlCommand(dbScript);

            dbScript = "DELETE FROM LocaleStringResource where ResourceName in ('MVP.Settings.ShowMVPOnHomepage','MVP.Settings.ShowMVPOnHomepage.hint','MVP.Settings.NumberOfMVPOnHomepage','MVP.Settings.NumberOfMVPOnHomepage.hint') ";
            Database.ExecuteSqlCommand(dbScript);
            SaveChanges();
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        public System.Collections.Generic.IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : BaseEntity, new()
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            throw new System.NotImplementedException();
        }

        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Detach an entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public void Detach(object entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            ((IObjectContextAdapter)this).ObjectContext.Detach(entity);
        }



        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether proxy creation setting is enabled (used in EF)
        /// </summary>
        public virtual bool ProxyCreationEnabled
        {
            get
            {
                return this.Configuration.ProxyCreationEnabled;
            }
            set
            {
                this.Configuration.ProxyCreationEnabled = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether auto detect changes setting is enabled (used in EF)
        /// </summary>
        public virtual bool AutoDetectChangesEnabled
        {
            get
            {
                return this.Configuration.AutoDetectChangesEnabled;
            }
            set
            {
                this.Configuration.AutoDetectChangesEnabled = value;
            }
        }
        #endregion

    }
}
