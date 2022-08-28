using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoAppBLL.DTO;
using ToDoAppDAL.Entities;
using Task = System.Threading.Tasks.Task;

namespace ToDoAppBLL.Interfaces
{
    public interface ITodoListService:IService<TodoList>
    {
        public Task<ToDoListDto> GetTodoListByIdAsync(int toDoListId);
        public Task UpdateAsync(ToDoListDto ToDoListDto);
        public Task AddAsync(ToDoListDto ToDoListDto);
        public Task<IEnumerable<ToDoListDto>> GetAllTodoListsAsync();
    }
}
