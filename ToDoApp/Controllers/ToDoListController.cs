using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using ToDoAppBLL.Interfaces;
using ToDoAppDAL.Entities;
using ToDoAppBLL.DTO;
using AutoMapper;
using FluentValidation;
using ToDoAppBLL.Validation;
using ETask = ToDoAppDAL.Entities.Task;
using Task = System.Threading.Tasks.Task;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITodoListService _todoListService;
        private readonly ToDoListDtoValidator _todoListValidator;

        public ToDoListController(IMapper mapper, ITodoListService todoListService, ToDoListDtoValidator validator)
        {
            _mapper = mapper;
            _todoListService = todoListService;
            _todoListValidator = validator;
        }

        // GET: api/tasks
        [HttpGet]
        public async Task<ActionResult> GetToDoLists()
        {
            var toDoListDtos = await _todoListService.GetAllTodoListsAsync();

            return Ok(toDoListDtos);
        }

        // GET api/tasks/1
        [HttpGet("{id}")]
        public async Task<ActionResult> GetToDoListById(int id)
        {
            var toDoListDto = await _todoListService.GetTodoListByIdAsync(id);
            if (!(toDoListDto is null))
            {
                return Ok(toDoListDto);
            }
            return NotFound();
        }

        // POST api/<TaskController>
        [HttpPost("{id}")]
        public async Task<ActionResult> PostToDoList([FromBody] ToDoListDto todoListDto)
        {
            var validationResult = _todoListValidator.Validate(todoListDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult);
            }
            await _todoListService.AddAsync(todoListDto);

            return CreatedAtAction("PostToDoList",
                new { id = todoListDto.TodoListId }, todoListDto);
        }

        // PUT api/<TaskController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutToDoList(int id, [FromBody] ToDoListDto toDoListDto)
        {
            var toDoListToUpdate = await _todoListService.GetTodoListByIdAsync(id);
            if (toDoListToUpdate == null)
                return NotFound($"ToDoList with id {id} doesn't exist/not found");
            await _todoListService.UpdateAsync(toDoListDto);
            return NoContent();
        }

        // DELETE api/<TaskController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _todoListService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}
