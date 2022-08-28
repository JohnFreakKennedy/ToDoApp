using ToDoAppDAL.UoW;
using Task = System.Threading.Tasks.Task;

namespace ToDoAppBLL.Interfaces
{
    public interface IService<in TBaseEntity>
    {
        public IUnitOfWork UnitOfWork { get; }
        public Task DeleteAsync(TBaseEntity entity);
        public Task DeleteByIdAsync(int id);
    }
}
