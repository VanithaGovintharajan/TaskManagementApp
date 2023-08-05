using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Models
{
    public class UserTask
    {
        public Guid Id { get; set; }

        [MaxLength(500)]
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime Deadline { get; set; }
        public string? ImageUrl { get; set; }
        public Guid TaskStateId { get; set; }

    }
}
