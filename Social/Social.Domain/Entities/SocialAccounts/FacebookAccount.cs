using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Domain.Entities
{
    public class FacebookAccount : SocialAccount
    {
        [Required]
        public bool IfConvertVisitorPostToConversation { get; set; } = true;

        [Required]
        public bool IfConvertWallPostToConversation { get; set; } = true;

        public string Category { get; set; }

        public string SignInAs { get; set; }
    }
}
