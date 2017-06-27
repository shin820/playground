namespace Social.Domain
{
    using Framework.Core;
    using Framework.EntityFramework;
    using log4net;
    using System.Data.Entity;

    public class SiteDataContext : DataContext
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(SiteDataContext));

        public SiteDataContext(string nameOrConnectionString, IUserContext userContext) : base(nameOrConnectionString, userContext)
        {
            Database.SetInitializer<SiteDataContext>(null);
            Database.Log = t => logger.Debug(t);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
