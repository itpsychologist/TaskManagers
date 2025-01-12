namespace TaskManagers.Models
{
    public class TaskType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<WorkTask> Tasks { get; set; }
    }
}
