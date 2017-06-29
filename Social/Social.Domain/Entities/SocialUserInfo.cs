using Framework.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Domain.Entities
{
    public class SocialUserInfo : EntityWithSite
    {
        [Required]
        [MaxLength(200)]
        public string SocialId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
