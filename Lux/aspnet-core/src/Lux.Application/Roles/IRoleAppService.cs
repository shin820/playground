using System.Threading.Tasks;
using Abp.Application.Services;
using Lux.Roles.Dto;
using Abp.Application.Services.Dto;

namespace Lux.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
        ListResultDto<RoleListDto> GetRoles();
    }
}
