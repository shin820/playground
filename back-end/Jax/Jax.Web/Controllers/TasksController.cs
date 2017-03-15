using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Jax.Tasks;
using Jax.Tasks.Dto;
using Jax.Users;
using Jax.Web.Models.Task;

namespace Jax.Web.Controllers
{
    public class TasksController : JaxControllerBase
    {
        private readonly ITaskAppService _taskAppService;
        private readonly IUserAppService _userAppService;

        public TasksController(
            ITaskAppService taskAppService,
            IUserAppService userAppService
            )
        {
            _taskAppService = taskAppService;
            _userAppService = userAppService;
        }

        public async Task<ActionResult> Index(GetAllTasksInput input)
        {
            var output = await _taskAppService.GetAll(input);
            var model = new IndexViewModel(output.Items)
            {
                SelectedTaskStatus = input.Status
            };

            return View(model);
        }

        public async Task<ActionResult> Create()
        {
            var userSelectListItems = (await _userAppService.GetUsers()).Items
                .Select(p => new SelectListItem { Text = p.Name, Value = p.Id.ToString() })
                .ToList();

            userSelectListItems.Insert(0, new SelectListItem { Value = string.Empty, Text = L("Unassigned"), Selected = true });

            return View(new CreateTaskViewModel(userSelectListItems));
        }
    }
}