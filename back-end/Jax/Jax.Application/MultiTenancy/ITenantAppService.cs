using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Jax.MultiTenancy.Dto;

namespace Jax.MultiTenancy
{
    public interface ITenantAppService : IApplicationService
    {
        ListResultDto<TenantListDto> GetTenants();

        Task CreateTenant(CreateTenantInput input);
    }
}
