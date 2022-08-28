using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDoAppDAL.Interfaces;
using ToDoAppDAL.Repositories;

namespace ToDoAppDAL.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ToDoDbContext _toDoDbContext = new ToDoDbContext();

        private TaskRepository _taskRepository;
        private TodoListRepository _todoListRepository;

        public ITaskRepository TaskRepository 
        { 
            get
            {
                if (_taskRepository == null) _taskRepository = new TaskRepository(_toDoDbContext);
                return _taskRepository;
            }
        }

        public ITodoListRepository TodoListRepository 
        {
            get
            {
                if (_todoListRepository == null) _todoListRepository = new TodoListRepository(_toDoDbContext);
                return _todoListRepository;
            }
        }

        public int Save()
        {
            return _toDoDbContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _toDoDbContext.SaveChangesAsync();
        }
    }
}
