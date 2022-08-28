using System;
using System.Collections.Generic;
using System.Text;
using ToDoAppDAL.Enums;
using ETask = ToDoAppDAL.Entities.Task;


namespace ToDoAppBLL.DTO
{
    public class ToDoListDto
    {
            public int TodoListId { get; set; }
            public string Title { get; set; }
            public ListStatus Status { get; set; } = ListStatus.Open;
            public ICollection<int> TasksIds { get; set; }
    }
}
