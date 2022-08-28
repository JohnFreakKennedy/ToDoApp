using System;
using System.Collections.Generic;
using System.Text; 
using FluentValidation;
using ToDoAppBLL.DTO;

namespace ToDoAppBLL.Validation
{
    public class ToDoListDtoValidator:AbstractValidator<ToDoListDto>
    {
        public ToDoListDtoValidator()
        {
            RuleFor(x => x.TodoListId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Title).NotEmpty().Length(1, 200)
                .WithMessage("List title can't be longer than 200 symbols");
            RuleFor(x => x.Status).IsInEnum();
            RuleFor(x=>x.TasksIds).NotEmpty();
        }
    }
}
