namespace TaskManagers.Models
{
    public class WorkTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; }
        public Priority Priority { get; set; }

        public int TaskTypeId { get; set; }
        public TaskType TaskType { get; set; }

        public ICollection<Worker> Assignees { get; set; }
    }
}
