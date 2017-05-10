using LiveChatApi.Entity.Chats;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChatApi.Repository
{
    public class LiveChatDbContext : DbContext
    {
        public LiveChatDbContext() : base("name=LiveChatDbContext")
        {
        }

        public IDbSet<Chat> Chats { get; set; }
        public IDbSet<ChatMessage> ChatMessages { get; set; }
        public IDbSet<ChatUser> ChatUsers { get; set; }
    }
}
