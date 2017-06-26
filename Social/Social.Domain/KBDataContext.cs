namespace KB.Domain
{
    using System.Data.Entity;

    public class KBDataContext : DbContext
    {
        public KBDataContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
