using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RedisTest.ServiceStack
{
    public abstract class RedisClientManagerTest : IDisposable
    {
        private IRedisClient _redis;
        private Todo _savedTodo = null;

        protected abstract IRedisClient GetRedisClient();

        public RedisClientManagerTest()
        {
            _redis = GetRedisClient();
        }

        public void Dispose()
        {
            if (_savedTodo != null)
            {
                _redis.As<Todo>().DeleteById(_savedTodo.Id);
            }
        }

        [Fact]
        public void ShouldStore()
        {
            // Arrange
            var todos = _redis.As<Todo>();
            var todo = new Todo
            {
                Id = todos.GetNextSequence(),
                Content = "Learn Redis",
                Order = 1
            };

            // Act
            Todo newTodo = todos.Store(todo);

            // Assert
            _savedTodo = todos.GetById(newTodo.Id);
            Assert.Equal(todo.Id, _savedTodo.Id);
            Assert.Equal(todo.Content, _savedTodo.Content);
            Assert.Equal(todo.Order, _savedTodo.Order);
            Assert.Equal(false, _savedTodo.Done);
        }

        [Fact]
        public void ShouldUpdate()
        {
            // Arrange
            var todos = _redis.As<Todo>();
            var todo = new Todo
            {
                Id = todos.GetNextSequence(),
                Content = "Learn Redis",
                Order = 1
            };

            // Act
            Todo newTodo = todos.Store(todo);
            Assert.Equal(false, newTodo.Done);
            newTodo.Done = true;
            newTodo = todos.Store(newTodo);

            // Assert
            _savedTodo = todos.GetById(newTodo.Id);
            Assert.Equal(true, _savedTodo.Done);
        }

        public class Todo
        {
            public long Id { get; set; }
            public string Content { get; set; }
            public int Order { get; set; }
            public bool Done { get; set; }
        }
    }
}
