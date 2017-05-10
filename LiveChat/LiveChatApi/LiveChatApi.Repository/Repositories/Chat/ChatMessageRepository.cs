using LiveChatApi.Entity.Chats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChatApi.Repository.Repositories.Chat
{
    public interface IChatMessageRepository : IRepositoryBase<ChatMessage>
    {

    }

    public class ChatMessageRepository : RepositoryBase<ChatMessage>, IChatMessageRepository
    {
        public ChatMessageRepository(LiveChatDbContext dbContext) : base(dbContext)
        {
        }
    }
}
