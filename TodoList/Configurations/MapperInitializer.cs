using AutoMapper;
using TodoList.Models;

namespace TodoList.Configurations
{
    /// <summary>
    /// Manages maps between entities
    /// </summary>
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<TodoItem, TodoItemRespose>();
        }
    }
}
