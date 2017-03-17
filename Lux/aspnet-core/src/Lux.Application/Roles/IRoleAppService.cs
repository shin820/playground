using System.Threading.Tasks;
using Abp.Application.Services;
using Lux.Roles.Dto;

namespace Lux.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
