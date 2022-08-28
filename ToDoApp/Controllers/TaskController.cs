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
    public class TaskController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITaskService _taskService;
        private readonly TaskDtoValidator _taskDtoValidator;

        public TaskController(IMapper mapper, ITaskService taskService, TaskDtoValidator validator)
        {
            _mapper = mapper;
            _taskService = taskService;
            _taskDtoValidator = validator;
        }

        // GET: api/tasks
        [HttpGet]
        public async Task<ActionResult> GetTasks()
        {
            var tasksDTO = await _taskService.GetAllTasksAsync();

            return Ok(tasksDTO);
        }

        // GET api/tasks/1
        [HttpGet("{id}")]
        public async Task<ActionResult> GetTask(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (!(task is null))
            {
                var taskDTO = _mapper.Map<TaskDto>(task);
                return Ok(taskDTO);
            }
            return NotFound();
        }

        // POST api/<TaskController>
        [HttpPost("{id}")]
        public async Task<ActionResult> PostTask(int id,[FromBody] TaskDto taskDto)
        {
            var validationResult = _taskDtoValidator.Validate(taskDto);
            if(!validationResult.IsValid)
            {
                return BadRequest(validationResult);
            }
            await _taskService.AddAsync(taskDto, id);

            return CreatedAtAction("PostTask", taskDto);
        }

        // PUT api/<TaskController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutTaskById(int id, [FromBody] TaskDto taskDto)
        {
            var taskToUpdate = await _taskService.GetTaskByIdAsync(id);
            if(taskToUpdate == null) 
                return NotFound($"Task with id {id} doesn't exist/not found");
            await _taskService.UpdateAsync(taskDto);
            return NoContent();
        }

        // DELETE api/<TaskController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _taskService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}
