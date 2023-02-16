namespace TodoList.Models
{
    public class AppUser
    {
        public string Id { get; init; }
        public ICollection<TodoItem> TodoItems { get; init; }

        public AppUser(string id)
        {
            Id = id;
        }

        public void AddTodoItem(TodoItem item)
        {
            TodoItems.Add(item);
        }
    }
}
