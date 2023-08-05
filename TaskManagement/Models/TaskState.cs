using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class TaskState
    {
        public Guid Id { get; set; }

        [MaxLength(500)]
        public string Name { get; set; }

        public List<UserTask> UserTasks { get; set; } = new List<UserTask>();
    }
}
