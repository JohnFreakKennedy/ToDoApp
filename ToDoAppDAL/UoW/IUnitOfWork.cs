using System;
using System.Collections.Generic;
using System.Text;
using ToDoAppDAL.Interfaces;
using System.Threading.Tasks;

namespace ToDoAppDAL.UoW
{
    public interface IUnitOfWork
    {
        public ITaskRepository TaskRepository { get; }
        public ITodoListRepository TodoListRepository { get; }
        public Task SaveAsync();
        public int Save();
    }
}

