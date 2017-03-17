using System.Threading.Tasks;
using Abp.Application.Services;
using Lux.Sessions.Dto;

namespace Lux.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
