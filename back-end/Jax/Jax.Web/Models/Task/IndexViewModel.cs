using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abp.Localization;
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

        public TaskStatus? SelectedTaskStatus { get; set; }

        public List<SelectListItem> GetTasksStateSelectListItems(Func<string, string> l)
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = l("AllTasks"),
                    Value = "",
                    Selected = SelectedTaskStatus == null
                }
            };

            list.AddRange(Enum.GetValues(typeof(TaskStatus))
                .Cast<TaskStatus>()
                .Select(state =>
                    new SelectListItem
                    {
                        Text = l($"TaskStatus_{state}"),
                        Value = state.ToString(),
                        Selected = state == SelectedTaskStatus
                    })
                );

            return list;
        }
    }
}