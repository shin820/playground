using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChatApi.Entity.Chats
{
    public class Chat: EFEntity,IHaveTenant
    {
        [Required]
        [MaxLength(200)]
        public string Subject { get; set; }
        [Required]
        public ChatStatus Status { get; set; }
        [Required]
        public DateTime CreateTime { get; set; }
        public List<ChatMessage> Messages { get; set; }
        public int TenantId { get; set; }
    }
}
