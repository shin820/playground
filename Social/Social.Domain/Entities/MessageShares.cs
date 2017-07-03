using Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Domain.Entities
{
    public class MessageShares : EntityWithSite
    {
        public int MessageId { get; set; }

        public string Link { get; set; }

        //public string Name { get; set; }

        //public string SocialId { get; set; }

        public virtual Message Message { get; set; }
    }
}
