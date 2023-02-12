namespace TodoList.Models
{
    public class TodoItem
    {
        public Guid Id { get; init; }
        public Guid AppUserId { get; set; }
        public string Content { get; init; }
        public DateTimeOffset Created { get; init; }
        public AppUser AppUser { get; set; }

        public TodoItem(string content, DateTimeOffset created)
        {
            Content = content;
            Created = created;
        }

        public TodoItem()
        {
        }
    }
}
