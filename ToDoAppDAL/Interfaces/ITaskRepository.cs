using System;
using System.Collections.Generic;
using ToDoAppDAL.Entities;
using System.Threading.Tasks;
using System.Text;
using Task = System.Threading.Tasks.Task;
using ETask = ToDoAppDAL.Entities.Task;

namespace ToDoAppDAL.Interfaces
{
    public interface ITaskRepository: IRepository<ETask>
    {
        public Task<IEnumerable<ETask>> GetAllAsync();
        public Task<ETask> GetByIdAsync(int entityID);
        public Task AddAsync(ETask task);
        public Task UpdateAsync(ETask task);
        public Task DeleteByIdAsync(int taskID);
    }
}
