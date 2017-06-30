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
    [Table("t_Social_SocialAccount")]
    public abstract class SocialAccount : EntityWithSite, IHaveCreation, IHaveModification
    {
        public SocialAccount()
        {
            Conversations = new List<Conversation>();
        }

        [Required]
        public string SocialId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Email { get; set; }

        public string Avatar { get; set; }

        [Required]
        public string Token { get; set; }

        public bool IfEnable { get; set; } = true;

        public DateTime CreatedTime { get; set; }

        public int CreatedBy { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }

        public int? ConversationDepartment { get; set; }

        public ConversationPriority? ConversationDepartmentPriority { get; set; }

        public int? ConversationAgent { get; set; }

        public ConversationPriority? ConversationAgentPriority { get; set; }

        public bool IfConvertMessageToConversation { get; set; } = true;

        public virtual IList<Conversation> Conversations { get; set; }
    }
}
