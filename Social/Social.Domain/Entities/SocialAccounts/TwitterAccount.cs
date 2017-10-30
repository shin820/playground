using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Domain.Entities
{
    public class TwitterAccount : SocialAccount
    {
        [Required]
        public bool IfConvertTweetToConversation { get; set; } = true;
    }
}
