using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly IToDoRepository _todoRepo;

        public TodoController(IToDoRepository todoRepo)
        {
            _todoRepo = todoRepo;
        }

        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            return _todoRepo.All;
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(long id)
        {
            var item = _todoRepo.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        /// <summary>
        /// Creates a TodoItem.
        /// </summary>
        /// <remarks>
        /// Note that the key is a GUID and not an integer.
        ///  
        ///     POST /Todo
        ///     {
        ///        "key": "0e7ad584-7788-4ab1-95a6-ca0a5b444cbb",
        ///        "name": "Item1",
        ///        "isComplete": true
        ///     }
        /// 
        /// </remarks>
        /// <param name="item"></param>
        /// <returns>New Created Todo Item</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [HttpPost]
        [ProducesResponseType(typeof(TodoItem), 201)]
        [ProducesResponseType(typeof(TodoItem), 400)]
        public IActionResult Post([FromBody]TodoItem item)
        {
            if (item == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            _todoRepo.Insert(item);

            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody]TodoItem item)
        {
            if (item == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var todo = _todoRepo.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _todoRepo.Update(item);

            return NoContent();
        }

        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _todoRepo.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _todoRepo.Delete(id);
            return new NoContentResult();
        }
    }
}
