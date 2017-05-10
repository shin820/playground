using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChatApi.Entity.Chats
{
    public class ChatMessage : EFEntity, IHaveTenant
    {
        public int ChatId { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public int TenantId { get; set; }
    }
}
