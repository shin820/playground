namespace Social.Domain
{
    using Framework.Core;
    using Framework.EntityFramework;
    using log4net;
    using Social.Domain.Entities;
    using System.Data.Entity;

    public class SiteDataContext : DataContext
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(SiteDataContext));

        public SiteDataContext(string nameOrConnectionString, IUserContext userContext) : base(nameOrConnectionString, userContext)
        {
            //Database.SetInitializer<SiteDataContext>(null);
            Database.Log = t => logger.Debug(t);
        }

        public virtual DbSet<Conversation> Conversations { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<IntegrationAccount> IntegrationAccounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Conversation>()
                .HasMany(t => t.Messages)
                .WithRequired(t => t.Conversation)
                .HasForeignKey(t => t.ConversationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Conversation>()
                .HasRequired(t => t.IntegrationAccount)
                .WithMany(t => t.Conversations)
                .HasForeignKey(t => t.IntegrationAccountId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>()
                .HasMany(t => t.Attachments)
                .WithRequired(t => t.Message)
                .HasForeignKey(t => t.MessageId)
                .WillCascadeOnDelete(false);
        }
    }
}
