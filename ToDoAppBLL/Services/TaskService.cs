using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoAppBLL.DTO;
using ToDoAppBLL.Interfaces;
using ToDoAppDAL.UoW;
using ETask = ToDoAppDAL.Entities.Task;
using Task = System.Threading.Tasks.Task;

namespace ToDoAppBLL.Services
{
    public class TaskService : ITaskService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                if (_unitOfWork == null)
                    _unitOfWork = new UnitOfWork();
                return _unitOfWork;
            }
        }

        public TaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ETask, TaskDto>().ReverseMap();
                cfg.CreateMap<ToDoAppDAL.Entities.TodoList, ToDoListDto>().ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public async Task AddAsync(TaskDto TaskDto, int toDoListId)
        { 
            var taskEntity = _mapper.Map<TaskDto,ETask>(TaskDto);
            taskEntity.TodoListId = toDoListId;
            taskEntity.TodoList = await _unitOfWork.TodoListRepository.GetByIdAsync(TaskDto.TodoListId);
            await _unitOfWork.TaskRepository.AddAsync(taskEntity);
            await _unitOfWork.SaveAsync();
        }

        public async Task AttachTaskToAnotherList(TaskDto TaskDto, int toDoListId)
        {
            var taskEntity = _mapper.Map<TaskDto,ETask>(TaskDto);
            taskEntity.TodoListId = toDoListId;
            taskEntity.TodoList = await _unitOfWork.TodoListRepository.GetByIdAsync(toDoListId);
            var toDoList = await _unitOfWork.TodoListRepository.GetByIdAsync(toDoListId);
            toDoList.Tasks.Add(taskEntity);
            await _unitOfWork.TodoListRepository.UpdateAsync(toDoList);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(ETask entity)
        {
            await _unitOfWork.TaskRepository.DeleteByIdAsync(entity.TaskId);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _unitOfWork.TaskRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<TaskDto> GetTaskByIdAsync(int taskId)
        {
            var taskEntity = await _unitOfWork.TaskRepository.GetByIdAsync(taskId);
            var TaskDto = _mapper.Map<ETask, TaskDto>(taskEntity);
            return TaskDto;
        }

        public async Task UpdateAsync(TaskDto TaskDto)
        {
            var taskEntity = _mapper.Map<TaskDto, ETask>(TaskDto);
            await _unitOfWork.TaskRepository.UpdateAsync(taskEntity);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync()
        {
            var tasksRepo = await _unitOfWork.TaskRepository.GetAllAsync();
            var tasks = _mapper.Map<List<ETask>, List<TaskDto>>((List<ETask>)tasksRepo);
            return tasks;
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksByListAsync(int toDoListId)
        {
            var tasksRepo = await _unitOfWork.TaskRepository.GetAllAsync();
            tasksRepo = tasksRepo.Where(x => x.TodoListId == toDoListId);
            var tasks = _mapper.Map<List<ETask>, List<TaskDto>>((List<ETask>)tasksRepo);
            return tasks;
        }
    }
}
