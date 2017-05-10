using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcDemo_CannedMessage.Models
{
    public class Pager
    {
        public int? Page { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
        public string OrderBy { get; set; }
        public bool IsAsc = false;
    }
}