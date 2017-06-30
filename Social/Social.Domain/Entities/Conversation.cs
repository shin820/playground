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
    [Table("t_Social_Conversation")]
    public class Conversation : EntityWithSite, IHaveModification
    {
        public Conversation()
        {
            Messages = new List<Message>();
        }

        [Required]
        public ConversationSource Source { get; set; }

        [Required]
        public string SocialId { get; set; }

        [Required]
        public bool IfRead { get; set; }

        public int? Agent { get; set; }

        public int? Department { get; set; }

        [Required]
        public ConversationStatus Status { get; set; } = ConversationStatus.New;

        [Required]
        [MaxLength(200)]
        public string Subject { get; set; }

        [MaxLength(2000)]
        public string Notes { get; set; }

        public int SocialAccountId { get; set; }

        public TimeSpan? HandlingTime { get; set; }

        [Required]
        public DateTime LastMessageSentTime { get; set; }

        public bool IfLastMessageSendByAgent { get; set; }

        public int LastMessageSendBy { get; set; }

        public int? LastRepliedAgent { get; set; }

        [Required]
        public ConversationPriority Priority { get; set; } = ConversationPriority.Normal;

        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedTime { get; set; }

        public virtual IList<Message> Messages { get; set; }

        public virtual SocialAccount SocialAccount { get; set; }

        //public virtual IList<Agent> RepliedAgents { get; set; }
    }
}
