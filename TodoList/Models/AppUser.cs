namespace TodoList.Models
{
    public class AppUser
    {
        public Guid Id { get; init; }
        public string GivenName { get; init; }
        public string FamilyName { get; init; }
        public string Nickname { get; init; }
        public string Picture { get; init; }
        public string Email { get; init; }
        public ICollection<TodoItem> TodoItems { get; init; }

        public AppUser(
            string givenName,
            string familyName,
            string nickname,
            string picture,
            string email)
        {
            GivenName = givenName;
            FamilyName = familyName;
            Nickname = nickname;
            Picture = picture;
            Email = email;
        }

        public void AddTodoItem(TodoItem item)
        {
            TodoItems.Add(item);
        }
    }
}
