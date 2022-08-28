using System;
using ToDoAppDAL.Enums;

namespace ToDoAppBLL.DTO
{
    public class TaskDto
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public DateTime? DueDate { get; set; }
        public RepeatType RepeatType { get; set; } = RepeatType.Once;
        public TaskStatus Status { get; set; } = TaskStatus.Todo;
        public string Description { get; set; }
        public int TodoListId { get; set; }
    }
}
