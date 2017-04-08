using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Lux.Authorization.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lux.Roles.Dto
{
    [AutoMapFrom(typeof(Role))]
    public class RoleListDto : EntityDto
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
