using System.Threading.Tasks;
using Abp.Application.Services;
using Jax.Roles.Dto;

namespace Jax.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
