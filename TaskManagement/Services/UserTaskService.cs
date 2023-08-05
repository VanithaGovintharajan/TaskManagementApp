using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TaskManagement.Models;
using TaskManagement.Services.Contracts;

namespace TaskManagement.Services
{
    public class UserTaskService : IUserTaskService
    {
        private readonly TaskManagementContext taskManagementContext;

        public UserTaskService(TaskManagementContext taskManagementContext)
        {
            this.taskManagementContext = taskManagementContext;
        }

        public IEnumerable<UserTask> GetAllUserTasks()
        {
            return taskManagementContext.UserTasks;
        }

        public UserTask? GetUserTaskById(Guid userTaskId)
        {
            if (userTaskId == Guid.Empty)
                throw new ArgumentException("id not found.");

            return taskManagementContext.UserTasks.FirstOrDefault(c => c.Id == userTaskId);
        }

        public UserTask CreateUserTask(UserTask userTask)
        {
            if (userTask == null)
                throw new ArgumentException("Task details not found.");

            userTask.Id = Guid.NewGuid();
            userTask.Name = userTask.Name;
            userTask.Description = userTask.Description;
            userTask.Deadline = userTask.Deadline;
            userTask.ImageUrl = userTask.ImageUrl;
            userTask.TaskStateId = userTask.TaskStateId;

            taskManagementContext.UserTasks.Add(userTask);
            taskManagementContext.SaveChanges();
            return userTask;
        }

        public UserTask UpdateUserTask(UserTask userTask)
        {
            var task = GetUserTask(userTask.Id) ?? throw new ArgumentException("Task not found.");
            task.Name = userTask.Name;
            task.Description = userTask.Description;
            task.Deadline = userTask.Deadline;
            task.ImageUrl = userTask.ImageUrl;
            task.TaskStateId = userTask.TaskStateId;

            taskManagementContext.SaveChanges();
            return task;

        }

        public bool DeleteUserTask(Guid userTaskId)
        {
            var task = GetUserTask(userTaskId) ?? throw new ArgumentException("Task not found.");
            taskManagementContext.UserTasks.Remove(task);
            taskManagementContext.SaveChanges();
            return true;
        }

        public UserTask UpdateTaskState(Guid userTaskId, Guid taskStateId)
        {
            var task = GetUserTask(userTaskId) ?? throw new ArgumentException("Task not found.");
            task.TaskStateId = taskStateId;
            taskManagementContext.SaveChanges();
            return task;
        }

        private UserTask GetUserTask(Guid userTaskId)
        {
            return taskManagementContext.UserTasks.FirstOrDefault(t => t.Id == userTaskId);
        }
    }
}
