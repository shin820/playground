using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jax.Tasks.Dto
{
    public class GetAllTasksInput
    {
        public Entities.TaskStatus? Status { get; set; }
    }
}
