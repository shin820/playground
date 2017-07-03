namespace Social.Domain
{
    using Framework.Core;
    using Framework.EntityFramework;
    using log4net;
    using Social.Domain.Entities;
    using Social.Infrastructure.Enum;
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
        public virtual DbSet<SocialAccount> SocialAccounts { get; set; }
        public virtual DbSet<SocialUserInfo> SocialUserInfoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Conversation>()
                .HasMany(t => t.Messages)
                .WithRequired(t => t.Conversation)
                .HasForeignKey(t => t.ConversationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Conversation>()
                .HasRequired(t => t.SocialAccount)
                .WithMany(t => t.Conversations)
                .HasForeignKey(t => t.SocialAccountId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>()
                .HasMany(t => t.Attachments)
                .WithRequired(t => t.Message)
                .HasForeignKey(t => t.MessageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>()
                .HasMany(t => t.Shares)
                .WithRequired(t => t.Message)
                .HasForeignKey(t => t.MessageId)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<SocialAccount>()
                .Map<FacebookAccount>(m => m.Requires("Type").HasValue(0).IsRequired())
                .Map<TwitterAccount>(m => m.Requires("Type").HasValue(1).IsRequired());
        }
    }
}
