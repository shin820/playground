using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChatApi.Entity.Chats
{
    public class ChatUser : EFEntity, IHaveTenant
    {
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public int TenantId { get; set; }
    }
}
