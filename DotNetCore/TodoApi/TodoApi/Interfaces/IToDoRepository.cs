using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Interfaces
{
    public interface IToDoRepository
    {
        bool DoesItemExist(long id);
        IEnumerable<TodoItem> All { get; }
        TodoItem Find(long id);
        void Insert(TodoItem item);
        void Update(TodoItem item);
        void Delete(long id);
    }
}
