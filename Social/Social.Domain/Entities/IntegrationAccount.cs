using Framework.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Domain.Entities
{
    //[Table("t_Social_IntegrationAccount")]
    public class IntegrationAccount : EntityWithSite, IHaveCreation, IHaveModification
    {
        public IntegrationAccount()
        {
            Conversations = new List<Conversation>();
        }

        public string SocialId { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public DateTime CreatedTime { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedTime { get; set; }

        public virtual IList<Conversation> Conversations { get; set; }
    }
}
