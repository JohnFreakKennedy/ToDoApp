using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ToDoAppDAL.Enums;

namespace ToDoAppDAL.Entities
{
    public class TodoList:TBaseEntity
    {
        [Required(ErrorMessage = "Entity id can not be null"), Range(0, Double.PositiveInfinity)]
        public int TodoListId { get; set; }
        [Required, MaxLength(200, ErrorMessage = "List title can't be longer than 200 symbols")]
        public string Title { get; set; }
        [Required, EnumDataType(typeof(ListStatus))]
        public ListStatus Status { get; set; } = ListStatus.Open;
        [Required]
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
