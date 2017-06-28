using Framework.Core;
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
    public class Message : EntityWithSite, IHaveModification
    {
        [Required]
        public int ConversationId { get; set; }

        [Required]
        public string SocialId { get; set; }

        public bool? IsReplyComment { get; set; }
        public string ParentCommentId { get; set; }

        public DateTime SendTime { get; set; }
        [MaxLength(200)]
        public string SendBy { get; set; }

        public int? Agent { get; set; }

        public string Content { get; set; }

        [MaxLength(500)]
        public string SocialUrl { get; set; }

        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedTime { get; set; }

        public virtual Conversation Conversation { get; set; }
    }
}
