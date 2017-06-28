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
    [Table("t_Social_Tag")]
    public class Tag : EntityWithSite, IHaveCreation
    {
        public Tag()
        {
            Conversations = new List<Conversation>();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public DateTime CreatedTime { get; set; }
        public int CreatedBy { get; set; }

        public virtual IList<Conversation> Conversations { get; set; }
    }
}
