using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Infrastructure.Facebook
{
    public class FbDataEntry
    {
        public string Id { get; set; }
        public long Time { get; set; }
        public List<FbMessage> Messaging { get; set; }
        public List<FbChange> Changes { get; set; }
    }
}
