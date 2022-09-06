using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ToDoAppBLL.DTO;
using ToDoAppBLL.Services;
using ToDoAppBLL.Interfaces;
using System.Linq;

namespace ToDoAppBLL.Validation
{
    public class ToDoListDtoValidator:AbstractValidator<ToDoListDto>
    {
        public ToDoListDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().Length(1, 200)
                .WithMessage("List title can't be longer than 200 symbols");
            RuleFor(x => x.Status).IsInEnum();
        }
    }
}
