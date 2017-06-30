using Framework.Core;
using Social.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Domain.Entities
{
    [Table("t_Social_Message")]
    public class Message : EntityWithSite
    {
        public Message()
        {
            Attachments = new List<MessageAttachment>();
        }

        [Required]
        public int ConversationId { get; set; }

        [Required]
        public MessageType Type { get; set; }

        [Required]
        public string SocialId { get; set; }

        public bool? IsReplyComment { get; set; }

        public string ParentCommentId { get; set; }

        public DateTime SendTime { get; set; }

        [Required]
        public int SenderId { get; set; }

        [MaxLength(200)]
        [Required]
        public string SenderSocialId { get; set; }

        [NotMapped]
        public string SenderEmail { get; set; }

        public bool IsSendByAgent { get; set; }

        public int? Agent { get; set; }

        public string Content { get; set; }

        [MaxLength(500)]
        public string SocialUrl { get; set; }

        public string FacebookConversationId { get; set; }

        public virtual Conversation Conversation { get; set; }

        public virtual IList<MessageAttachment> Attachments { get; set; }
    }
}
