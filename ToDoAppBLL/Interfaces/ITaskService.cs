using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoAppBLL.DTO;
using ETask = ToDoAppDAL.Entities.Task;
using Task = System.Threading.Tasks.Task;

namespace ToDoAppBLL.Interfaces
{
    public interface ITaskService:IService<ETask>
    {
        public Task<TaskDto> GetTaskByIdAsync(int taskId);
        public Task<IEnumerable<TaskDto>> GetAllTasksAsync();
        public Task<IEnumerable<TaskDto>> GetAllTasksByListAsync(int toDoListId);
        public Task UpdateAsync(TaskDto TaskDto);
        public Task AddAsync(TaskDto TaskDto, int toDoListId);
        public Task AttachTaskToAnotherList(TaskDto TaskDto, int toDoListId);
    }
}
