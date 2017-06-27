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
            Tags = new List<Tag>();
        }

        [Required]
        public ConversationSource Source { get; set; }

        [Required]
        public string SocialId { get; set; }

        [Required]
        public bool IsRead { get; set; }

        [Required]
        public TimeSpan HandlingTime { get; set; }

        [Required]
        public string Requester { get; set; }

        [Required]
        public string Receiver { get; set; }

        public int? AgentAssignee { get; set; }

        public int? DepartmentAssignee { get; set; }

        [Required]
        public ConversationStatus Status { get; set; } = ConversationStatus.New;

        [Required]
        [MaxLength(300)]
        public string Subject { get; set; }

        [MaxLength(2000)]
        public string Comment { get; set; }

        [Required]
        public ConversationPriority Priority { get; set; } = ConversationPriority.Normal;

        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedTime { get; set; }

        public virtual IList<Message> Messages { get; set; }
        public virtual IList<Tag> Tags { get; set; }
    }
}
