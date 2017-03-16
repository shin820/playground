using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jax.Entities;
using Jax.EntityFramework;
using Jax.Users;
using Microsoft.AspNet.Identity;
using Task = Jax.Entities.Task;
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
            var neo = new User
            {
                UserName = "Neo",
                Name = "Neo",
                Surname = "Neo",
                EmailAddress = "Neo@neo.com",
                IsEmailConfirmed = true,
                TenantId = 1,
                Password = new PasswordHasher().HashPassword(User.DefaultPassword)
            };
            _context.Users.Add(neo);
            _context.SaveChanges();

            _context.Tasks.AddRange(new List<Task>
            {
                new Task("Follow the white rabbit", "Follow the white rabbit in order to know the reality.", neo.Id),
                new Task("Clean your room") {Status = TaskStatus.Completed}
            });
        }
    }
}
