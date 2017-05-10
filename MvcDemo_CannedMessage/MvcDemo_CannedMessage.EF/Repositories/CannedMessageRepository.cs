using MvcDemo_CannedMessage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcDemo_CannedMessage.EF;

namespace MvcDemo_CannedMessage.Repository.Repositories
{
    public class CannedMessageRepository : RepositoryBase<CannedMessage>, ICannedMessageRepository
    {
        public CannedMessageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }

    public interface ICannedMessageRepository : IRepositoryBase<CannedMessage>
    {

    }
}
