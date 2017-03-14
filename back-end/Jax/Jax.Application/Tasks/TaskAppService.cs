using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using AutoMapper;
using Jax.Tasks.Dto;
using Task = Jax.Entities.Task;

namespace Jax.Tasks
{
    public class TaskAppService : ITaskAppService
    {
        private readonly IRepository<Task> _taskRepository;

        public TaskAppService(IRepository<Task> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public ListResultDto<TaskListDto> GetAll(GetAllTasksInput input)
        {
            var tasks = _taskRepository
                .GetAll()
                .WhereIf(input.Status.HasValue, t => t.Status == input.Status.Value)
                .OrderByDescending(t => t.CreationTime)
                .ToList();

            return new ListResultDto<TaskListDto>(
                Mapper.Map<List<TaskListDto>>(tasks)
                );
        }
    }
}
