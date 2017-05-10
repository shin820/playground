using LiveChatApi.Entity.Chats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChatApi.Repository.Repositories.Chat
{
    public interface IChatUserRepository : IRepositoryBase<ChatUser>
    {

    }

    public class ChatUserRepository : RepositoryBase<ChatUser>, IChatUserRepository
    {
        public ChatUserRepository(LiveChatDbContext dbContext) : base(dbContext)
        {
        }
    }
}
