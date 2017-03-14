using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jax.Tasks;
using Jax.Tasks.Dto;
using Jax.Web.Models.Task;

namespace Jax.Web.Controllers
{
    public class TasksController : JaxControllerBase
    {
        private readonly ITaskAppService _taskAppService;

        public TasksController(ITaskAppService taskAppService)
        {
            _taskAppService = taskAppService;
        }

        public ActionResult Index(GetAllTasksInput input)
        {
            var output = _taskAppService.GetAll(input);
            var model = new IndexViewModel(output.Items);

            return View(model);
        }
    }
}