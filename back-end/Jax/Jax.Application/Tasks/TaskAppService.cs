using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public async Task<ListResultDto<TaskListDto>> GetAll(GetAllTasksInput input)
        {
            var tasks = await _taskRepository
                .GetAll()
                .Include(t => t.AssignedUser)
                .WhereIf(input.Status.HasValue, t => t.Status == input.Status.Value)
                .OrderByDescending(t => t.CreationTime)
                .ToListAsync();

            return new ListResultDto<TaskListDto>(
                Mapper.Map<List<TaskListDto>>(tasks)
                );
        }

        public async System.Threading.Tasks.Task Create(CreateTaskInput input)
        {
            var task = Mapper.Map<Task>(input);
            await _taskRepository.InsertAsync(task);
        }
    }
}
