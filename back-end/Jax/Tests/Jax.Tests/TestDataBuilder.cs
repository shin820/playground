using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jax.EntityFramework;
using Task=Jax.Entities.Task;
using TaskStatus = Jax.Entities.TaskStatus;

namespace Jax.Tests
{
    public class TestDataBuilder
    {
        private readonly JaxDbContext _context;
        public TestDataBuilder(JaxDbContext context)
        {
            _context = context;
        }

        public void Build()
        {
            _context.Tasks.AddRange(new List<Task>
            {
                new Task("Follow the white rabbit", "Follow the white rabbit in order to know the reality."),
                new Task("Clean your room") {Status = TaskStatus.Completed}
            });
        }
    }
}
