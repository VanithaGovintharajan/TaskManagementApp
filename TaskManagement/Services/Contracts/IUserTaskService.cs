using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TaskManagement.Models;

namespace TaskManagement.Services.Contracts
{
    public interface IUserTaskService
    {
        IEnumerable<UserTask> GetAllUserTasks();
        UserTask GetUserTaskById(Guid userTaskId);
        UserTask CreateUserTask(UserTask userTask);
        UserTask UpdateUserTask(UserTask userTask);
        bool DeleteUserTask(Guid userTaskId);       
    }
}
