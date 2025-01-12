namespace TaskManagers.Models
{
    public class DashboardViewModel
    {
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int UrgentTasks { get; set; }
        public int TotalWorkers { get; set; }
        public List<WorkTask> RecentTasks { get; set; }
        public Dictionary<string, int> TasksByType { get; set; }
        public Dictionary<string, int> TasksByPriority { get; set; }
    }
}
