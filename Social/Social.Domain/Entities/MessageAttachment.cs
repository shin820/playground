using Framework.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Domain.Entities
{
    [Table("t_Social_MessageAttachment")]
    public class MessageAttachment : EntityWithSite
    {
        public int MessageId { get; set; }
        public string SocialId { get; set; }
        public string Name { get; set; }
        public string MimeType { get; set; }
        public int Size { get; set; }
        public string FileUrl { get; set; }

        public long? ImageWidth { get; set; }
        public long? ImageHeight { get; set; }
        public long? ImageMaxWidth { get; set; }
        public long? ImageMaxHeight { get; set; }
        public string ImageUrl { get; set; }
        public string ImagePreviewUrl { get; set; }

        public virtual Message Message { get; set; }
    }
}
