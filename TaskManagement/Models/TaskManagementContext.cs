using Microsoft.EntityFrameworkCore;

namespace TaskManagement.Models
{
    public class TaskManagementContext : DbContext
    {
        public TaskManagementContext(DbContextOptions<TaskManagementContext> options) : base(options)         
        {
            
        }
        
        public DbSet<UserTask> UserTasks { get; set;}

        public DbSet<TaskState> TaskStates { get; set;}
    }
}
