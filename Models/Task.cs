using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskManagers.Models
{
    public class Task
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; }

        [Required]
        public string Priority { get; set; } // Examples: Urgent, High, Low.

        // Navigation Properties
        public int TaskTypeId { get; set; }
        public TaskType TaskType { get; set; }
        public List<Worker> Assignees { get; set; }
    }
}
