using System;
using System.Collections.Generic;
using ToDoAppDAL.Entities;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using ETask = ToDoAppDAL.Entities.Task;
using System.Text;

namespace ToDoAppDAL.Interfaces
{
    public interface ITodoListRepository
    {
        public Task<IEnumerable<TodoList>> GetAllAsync();
        public Task<TodoList> GetByIdAsync(int toDoListID);
        public IEnumerable<ETask> GetTasks(int toDoListID);
        public Task AddAsync(TodoList todoList);
        public Task UpdateAsync(TodoList todoList);
        public Task DeleteByIdAsync(int toDoListID);
    }
}
