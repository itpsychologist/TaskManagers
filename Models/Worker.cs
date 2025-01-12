namespace TaskManagers.Models
{
    public class Worker
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int PositionId { get; set; }
        public Position Position { get; set; }

        public ICollection<WorkTask> AssignedTasks { get; set; }
    }
}

