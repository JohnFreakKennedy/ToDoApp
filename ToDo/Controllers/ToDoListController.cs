using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using ToDoAppBLL.Interfaces;
using ToDoAppDAL.Entities;
using ToDoAppBLL.DTO;
using AutoMapper;
using FluentValidation.AspNetCore;
using ToDoAppBLL.Validation;
using ETask = ToDoAppDAL.Entities.Task;
using Task = System.Threading.Tasks.Task;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]/[action]")]
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

        // GET: api/ToDoList/GetToDoLists
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
            if(!ModelState.IsValid)
            {
                var validationResult = _todoListValidator.Validate(todoListDto);
                return BadRequest(validationResult);
            }
            await _todoListService.AddAsync(todoListDto);

            return CreatedAtAction(nameof(GetToDoListById), new { id = todoListDto.TodoListId},todoListDto);
        }

        // PUT api/ToDoListController/PutToDoList/id
        [HttpPut("{id}")]
        public async Task<ActionResult> PutToDoList(int id, [FromBody] ToDoListDto toDoListDto)
        {
            var toDoListToUpdate = await _todoListService.GetTodoListByIdAsync(id);
            if (toDoListToUpdate == null)
                return NotFound($"ToDoList with id {id} doesn't exist/not found");
            if (ModelState.IsValid)
            {
                await _todoListService.UpdateAsync(toDoListDto);
                return NoContent();
            }
            return BadRequest($" {toDoListDto} Validation failed");
        }

        // DELETE api/<TaskController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (await _todoListService.GetTodoListByIdAsync(id) == null)
                return NotFound($"ToDoList with id {id} doesn't exist/not found");
            await _todoListService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}
