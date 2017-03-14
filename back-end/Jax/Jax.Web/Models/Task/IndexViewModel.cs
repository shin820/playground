using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Jax.Entities;
using Jax.Tasks.Dto;

namespace Jax.Web.Models.Task
{
    public class IndexViewModel
    {
        public IReadOnlyCollection<TaskListDto> Tasks { get; }

        public IndexViewModel(IReadOnlyCollection<TaskListDto> tasks)
        {
            Tasks = tasks;
        }

        public string GetTaskLabel(TaskListDto task)
        {
            switch (task.Status)
            {
                case TaskStatus.Open:
                    return "label-success";
                default:
                    return "label-default";
            }
        }
    }
}