using System;
using System.Collections.Generic;
using ToDoAppDAL.Entities;
using System.Text;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace ToDoAppDAL.Interfaces
{
    public interface IRepository<TBaseEntity>
    {
        public Task<IEnumerable<TBaseEntity>> GetAllAsync();
        public Task<TBaseEntity> GetByIdAsync(int entityID);
        public Task AddAsync(TBaseEntity entity);
        public Task UpdateAsync(TBaseEntity entity);
        public Task DeleteByIdAsync(int EntityID);
    }
}
