using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jax.Tasks;
using Jax.Tasks.Dto;
using Shouldly;
using Xunit;
using TaskStatus = Jax.Entities.TaskStatus;

namespace Jax.Tests.Tasks
{
    public class TaskAppService_Tests : JaxTestBase
    {
        private readonly ITaskAppService _taskAppService;

        public TaskAppService_Tests()
        {
            _taskAppService = Resolve<ITaskAppService>();
        }

        [Fact]
        public void Should_Get_All_Tasks()
        {
            //Act
            var output = _taskAppService.GetAll(new GetAllTasksInput());

            //Assert
            output.Items.Count.ShouldBe(2);
        }

        [Fact]
        public void Should_Get_Filtered_Tasks()
        {
            //Act
            var output = _taskAppService.GetAll(new GetAllTasksInput { Status = TaskStatus.Open });

            //Assert
            output.Items.ShouldAllBe(t => t.Status == TaskStatus.Open);
        }
    }
}
