using FluentValidation;
using ToDoAppBLL.DTO;
using System;
using ToDoAppDAL.Enums;

namespace ToDoAppBLL.Validation
{
    public class TaskDtoValidator:AbstractValidator<TaskDto>
    {
        public TaskDtoValidator()
        {
            RuleFor(x => x.TaskId).NotEmpty().GreaterThan(0);  
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("Please, specify a name for task")
                .Length(1,200);
            RuleFor(x => x.Description).Length(0, 500)
                .WithMessage("Task description can't be longer than 500 symbols");
            RuleFor(x => x.Status).NotEmpty().IsInEnum();
            RuleFor(x => x.RepeatType).NotEmpty().IsInEnum();
            RuleFor(x => x.TodoListId).NotEmpty()
                .WithMessage("Task must be assigned to any list");
            RuleFor(x=>x.DueDate).GreaterThan(System.DateTime.Now)
                .When(x=>x.DueDate!=null);
        }
    }
}
