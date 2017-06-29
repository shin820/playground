using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Infrastructure.Facebook
{
    public class FbMessage
    {
        public FbProfile Sender { get; set; }
        public FbProfile Recipient { get; set; }
        public long Timestamp { get; set; }
        public FbMessageContent Message { get; set; }
        public FbMessageDelivery Delivery { get; set; }
    }
}
