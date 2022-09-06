using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using ToDoAppDAL.Entities;
using ToDoAppDAL.Interfaces;
using Task = System.Threading.Tasks.Task;
using ETask = ToDoAppDAL.Entities.Task;

namespace ToDoAppDAL.Repositories
{
    public class TodoListRepository : ITodoListRepository
    {
        private readonly ToDoDbContext _toDoDbContext;
        public TodoListRepository(ToDoDbContext toDoDbContext)
        {
            _toDoDbContext = toDoDbContext;
        }
        public async Task AddAsync(TodoList todoList)
        {
            await _toDoDbContext.Database.OpenConnectionAsync();
            if(todoList.TodoListId != null && todoList.TodoListId>0)
                try
                {
                    await _toDoDbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.TodoLists ON");
                    await _toDoDbContext.AddAsync(todoList);
                    await _toDoDbContext.SaveChangesAsync();
                    await _toDoDbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.TodoLists OFF");
                    return;
                }
                finally
                {
                    _toDoDbContext.Database.CloseConnection();
                }
            await _toDoDbContext.TodoLists.AddAsync(todoList);
            await _toDoDbContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int toDoListID)
        {
            var entity = await _toDoDbContext.TodoLists.FirstOrDefaultAsync( x => x.TodoListId == toDoListID);
            if (entity != null) _toDoDbContext.Remove(entity);
            await _toDoDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TodoList>> GetAllAsync()
        {
            var todoLists = await _toDoDbContext.TodoLists.ToListAsync();
            return todoLists;
        }

        public async Task<TodoList> GetByIdAsync(int toDoListID)
        {
            try
            {
                return await _toDoDbContext.TodoLists.FirstOrDefaultAsync(x => x.TodoListId == toDoListID);
            }
            catch (InvalidCastException ex)
            {
                return null;
            }
        }

        public IEnumerable<ETask> GetTasks(int toDoListID)
        {

            return _toDoDbContext.Tasks.Where(t=>t.TodoListId == toDoListID).OrderBy(t=>t.Status).ToList();
        }

        public async Task UpdateAsync(TodoList todoList)
        {
            
            _toDoDbContext.Update(todoList);
            _toDoDbContext.Entry(todoList).State = EntityState.Modified;
            await _toDoDbContext.SaveChangesAsync();
        }
    }
}
