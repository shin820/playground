using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChatApi.Entity;

namespace LiveChatApi.Repository.Repositories.Chat
{
    public interface IChatRepository : IRepositoryBase<Entity.Chats.Chat>
    {

    }

    public class ChatRepository : RepositoryBase<Entity.Chats.Chat>, IChatRepository
    {
        public ChatRepository(LiveChatDbContext dbContext) : base(dbContext)
        {
        }
    }
}
