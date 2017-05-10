using MvcDemo_CannedMessage.Entity;
using System.Data.Entity;

namespace MvcDemo_CannedMessage.EF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("name=Default")
        {
           
        }

        public DbSet<CannedMessage> CannedMessages { get; set; }
    }
}