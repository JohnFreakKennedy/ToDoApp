using System.Linq;
using AutoMapper;
using ToDoAppDAL.Entities;
using ETask = ToDoAppDAL.Entities.Task;
using ToDoAppBLL.DTO;

namespace ToDoAppBLL
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
           CreateMap<ETask,TaskDto>().ReverseMap();

           CreateMap<TodoList,ToDoListDto>()
                .ForMember(destination => destination.TasksIds, options=>options
                .MapFrom(source=>source.Tasks
                .Select(t=>t.TaskId).ToList()))
                .ReverseMap();
        }
    }
}
