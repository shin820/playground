using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jax.Users;

namespace Jax.Web.Models.Task
{
    public class CreateTaskViewModel
    {
        public List<SelectListItem> Users { get; set; }

        public CreateTaskViewModel(List<SelectListItem> users)
        {
            Users = users;
        }
    }
}