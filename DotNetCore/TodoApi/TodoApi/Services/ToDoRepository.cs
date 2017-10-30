using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Interfaces;
using TodoApi.Models;

namespace TodoApi.Services
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly TodoContext _context;
        public ToDoRepository(TodoContext context)
        {
            _context = context;

            if (_context.TodoItems.Count() == 0)
            {
                InitializeData();
            }
        }

        public IEnumerable<TodoItem> All
        {
            get { return _context.TodoItems.ToList(); }
        }

        public bool DoesItemExist(long id)
        {
            return _context.TodoItems.Any(item => item.Id == id);
        }

        public TodoItem Find(long id)
        {
            return _context.TodoItems.FirstOrDefault(item => item.Id == id);
        }

        public void Insert(TodoItem item)
        {
            _context.TodoItems.Add(item);
            _context.SaveChanges();
        }

        public void Update(TodoItem item)
        {
            var todoItem = this.Find(item.Id);
            todoItem.Name = item.Name;
            todoItem.Notes = item.Notes;
            todoItem.IsComplete = item.IsComplete;
            _context.TodoItems.Update(todoItem);
            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var todoItem = this.Find(id);
            _context.TodoItems.Remove(todoItem);
            _context.SaveChanges();
        }

        private void InitializeData()
        {
            var todoItem1 = new TodoItem
            {
                Name = "Learn app development",
                Notes = "Attend Xamarin University",
                IsComplete = true
            };

            var todoItem2 = new TodoItem
            {
                Name = "Develop apps",
                Notes = "Use Xamarin Studio/Visual Studio",
                IsComplete = false
            };

            var todoItem3 = new TodoItem
            {
                Name = "Publish apps",
                Notes = "All app stores",
                IsComplete = false,
            };

            _context.TodoItems.Add(todoItem1);
            _context.TodoItems.Add(todoItem2);
            _context.TodoItems.Add(todoItem3);
            _context.SaveChanges();
        }
    }
}
