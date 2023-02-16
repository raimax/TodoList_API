namespace TodoList.Models
{
    public record TodoItemRespose
    {
        public Guid Id { get; init; }
        public string Content { get; init; }
        public string AppUserId { get; init; }
        public DateTimeOffset Created { get; init; }
    }
}
