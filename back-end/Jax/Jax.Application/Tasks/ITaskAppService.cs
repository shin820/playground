using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Jax.Tasks.Dto;

namespace Jax.Tasks
{
    public interface ITaskAppService : IApplicationService
    {
        ListResultDto<TaskListDto> GetAll(GetAllTasksInput input);
    }
}
