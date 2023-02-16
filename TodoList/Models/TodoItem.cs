namespace TodoList.Models
{
    public class TodoItem
    {
        public Guid Id { get; init; }
        public string AppUserId { get; init; }
        public string Content { get; init; }
        public DateTimeOffset Created { get; init; }
        public AppUser AppUser { get; init; }
    }
}
