using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.MultiTenancy;
using JetBrains.Annotations;

namespace Jax.Entities
{
    public class PermissionDefinition : Entity, IMayHaveTenant
    {
        public int? TenantId { get; set; }
        public string Name { get; set; }
        public string LocalizationName { get; set; }
        public string Description { get; set; }
        public string LocalizationDescription { get; set; }
        public int? ParentId { get; set; }
        public bool IsTenantSide { get; set; }
        public bool IsHostSide { get; set; }
    }
}
