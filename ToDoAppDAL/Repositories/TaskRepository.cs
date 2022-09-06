using System;
using System.Collections.Generic;
using ToDoAppDAL.Interfaces;
using ToDoAppDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Task = System.Threading.Tasks.Task;
using ETask = ToDoAppDAL.Entities.Task;

namespace ToDoAppDAL.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ToDoDbContext _toDoDbContext;
        public TaskRepository(ToDoDbContext toDoDbContext)
        {
            _toDoDbContext = toDoDbContext;
        }

        public async Task AddAsync(ETask task)
        {
            await _toDoDbContext.Tasks.AddAsync(task);
            await _toDoDbContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int taskID)
        {
            var task = await _toDoDbContext.Tasks.FirstOrDefaultAsync(x => x.TaskId == taskID);
            if (task != null) _toDoDbContext.Tasks.Remove(task);
            await _toDoDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ETask>> GetAllAsync()
        {
            var tasks = await _toDoDbContext.Tasks.ToListAsync();
            return tasks.OrderBy(t => t.Status);
        }

        public async Task<ETask> GetByIdAsync(int entityID)
        {
            return await _toDoDbContext.Tasks.FirstOrDefaultAsync(x => x.TaskId == entityID);
        }

        public async Task UpdateAsync(ETask task)
        {
            _toDoDbContext.Entry(task).State = EntityState.Modified;      
            await _toDoDbContext.SaveChangesAsync();
        }
    }
}
