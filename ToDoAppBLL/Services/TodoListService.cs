using System;
using AutoMapper;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using ToDoAppBLL.DTO;
using ToDoAppBLL.Interfaces;
using ToDoAppBLL.Exceptions;
using ToDoAppDAL.Entities;
using ToDoAppDAL.UoW;
using Task = System.Threading.Tasks.Task;

namespace ToDoAppBLL.Services
{
    public class TodoListService : ITodoListService
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

        public TodoListService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TodoList, ToDoListDto>().ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public async Task AddAsync(ToDoListDto ToDoListDto)
        {
            if (ToDoListDto.TodoListId == null)
            {
                var lists = await GetAllTodoListsAsync();
                ToDoListDto.TodoListId = lists.Max(tdl => tdl.TodoListId) + 1;
            }
            var toDoListEntity = _mapper.Map<ToDoListDto, TodoList>(ToDoListDto);
            await _unitOfWork.TodoListRepository.AddAsync(toDoListEntity);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(TodoList entity)
        {
            try
            {
                await _unitOfWork.TodoListRepository.DeleteByIdAsync((int)entity.TodoListId);
            }
            catch(InvalidCastException ex)
            {
                return;
            }
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _unitOfWork.TodoListRepository.DeleteByIdAsync(id);
        }

        public async Task<ToDoListDto> GetTodoListByIdAsync(int toDoListId)
        {
            var toDoList = await _unitOfWork.TodoListRepository.GetByIdAsync(toDoListId);
            var ToDoListDto = _mapper.Map<TodoList, ToDoListDto>(toDoList);
            return ToDoListDto; 
        }

        public async Task UpdateAsync(ToDoListDto toDoListDto)
        {
            try 
            { 
            var existingTodoList = await _unitOfWork.TodoListRepository.GetByIdAsync((int) toDoListDto.TodoListId);
            if (existingTodoList == null) throw new NotFoundException(nameof(toDoListDto), "Entity doesn't exist in a database");
            var mergedTodoList = _mapper.Map(toDoListDto, existingTodoList);
            await _unitOfWork.TodoListRepository.UpdateAsync(mergedTodoList);
            await _unitOfWork.SaveAsync();
            }
            catch (InvalidCastException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
            
        }

        public async Task<IEnumerable<ToDoListDto>> GetAllTodoListsAsync()
        {
            var todoListRepo = await _unitOfWork.TodoListRepository.GetAllAsync();
            return _mapper.Map<List<TodoList>, List<ToDoListDto>>((List<TodoList>)todoListRepo);
        }
    }
}
