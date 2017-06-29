using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Infrastructure.Facebook
{
    public class FbData
    {
        public string Object { get; set; }
        public List<FbDataEntry> Entry { get; set; }
    }
}
