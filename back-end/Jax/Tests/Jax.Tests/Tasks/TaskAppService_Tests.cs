﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Runtime.Validation;
using Jax.Tasks;
using Jax.Tasks.Dto;
using Shouldly;
using Xunit;
using TaskStatus = Jax.Entities.TaskStatus;
using Task = Jax.Entities.Task;

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
        public async System.Threading.Tasks.Task Should_Get_All_Tasks()
        {
            //Act
            var output = await _taskAppService.GetAll(new GetAllTasksInput());

            //Assert
            output.Items.Count.ShouldBe(2);
            output.Items.Count(t => t.AssignedUserName != null).ShouldBe(1);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_Get_Filtered_Tasks()
        {
            //Act
            var output = await _taskAppService.GetAll(new GetAllTasksInput { Status = TaskStatus.Open });

            //Assert
            output.Items.ShouldAllBe(t => t.Status == TaskStatus.Open);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_Create_New_Task_With_Title()
        {
            await _taskAppService.Create(new CreateTaskInput
            {
                Title = "Newly created task #1"
            });

            UsingDbContext(context =>
            {
                var task1 = context.Tasks.FirstOrDefault(t => t.Title == "Newly created task #1");
                task1.ShouldNotBeNull();
            });
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_Create_New_Task_With_Title_And_Assigned_Person()
        {
            var neo = UsingDbContext(context => context.Users.Single(p => p.Name == "Neo"));

            await _taskAppService.Create(new CreateTaskInput
            {
                Title = "Newly created task #1",
                AssignedUserId = neo.Id
            });

            UsingDbContext(context =>
            {
                var task1 = context.Tasks.FirstOrDefault(t => t.Title == "Newly created task #1");
                task1.ShouldNotBeNull();
                task1.AssignedUserId.ShouldBe(neo.Id);
            });
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_Not_Create_New_Task_Without_Title()
        {
            await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                await _taskAppService.Create(new CreateTaskInput
                {
                    Title = null
                });
            });
        }
    }
}
