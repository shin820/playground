using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChatApi.Entity
{
    public interface IHaveTenant
    {
        int TenantId { get; set; }
    }
}
