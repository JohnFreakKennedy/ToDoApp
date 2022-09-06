using System;
using ToDoAppDAL.Enums;
using System.ComponentModel.DataAnnotations;
using ToDoAppDAL.Validation;

namespace ToDoAppDAL.Entities
{ 
    public class Task:TBaseEntity
    {
        [Key]
        public int TaskId { get; set; }
        [Required,MaxLength(200, ErrorMessage = "Task name can't be longer than 200 symbols")]
        public string Name { get; set; }
        [FutureDate(ErrorMessage ="Invalid date, you can put only future date")]
        public DateTime? DueDate { get; set; }
        [Required,EnumDataType(typeof(RepeatType))]
        public RepeatType RepeatType { get; set; } = RepeatType.Once;
        [Required, EnumDataType(typeof(TaskStatus))]
        public TaskStatus Status { get; set; } = TaskStatus.Todo;
        [MaxLength(500, ErrorMessage = "Task description can't be longer than 500 symbols")]
        public string Description { get; set; }
        public TodoList TodoList { get; set; }
        public int TodoListId { get; set; }
    }
}
