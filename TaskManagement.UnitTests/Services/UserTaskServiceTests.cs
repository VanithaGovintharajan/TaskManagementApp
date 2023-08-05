using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Models;
using TaskManagement.Services;

namespace TaskManagement.UnitTests.Services
{
    [TestClass]
    public class UserTaskServiceTests
    {

        [TestMethod]
        public async Task GetAllUserTasks_ReturnValue()
        {
            var automocker = new AutoMocker();

            var userTask = new List<UserTask>
            {
                new UserTask()
                {
                    Id = Guid.NewGuid(),
                    Name = "Task Name",
                    Description = "Create new task",
                    Deadline = DateTime.Now,
                    ImageUrl = "",
                    TaskStateId = Guid.NewGuid()
                }
            };

            using (var dataContext = new TaskManagementContext(TestHelper.GetTaskManagementDataContextOptions()))
            {
                dataContext.UserTasks.AddRange(userTask);
                dataContext.SaveChanges();
                automocker.Use(dataContext);

                var service = automocker.CreateInstance<UserTaskService>();

                var result = service.GetAllUserTasks();

                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        public async Task GetUserTaskById_ReturnValue()
        {
            var automocker = new AutoMocker();
            var userTaskId = Guid.NewGuid();
            var userTask = new UserTask()
            {
                Id = userTaskId,
                Name = "Task Name",
                Description = "Create new task",
                Deadline = DateTime.Now,
                ImageUrl = "",
                TaskStateId = Guid.NewGuid()
            };

            using (var dataContext = new TaskManagementContext(TestHelper.GetTaskManagementDataContextOptions()))
            {
                dataContext.UserTasks.AddRange(userTask);
                dataContext.SaveChanges();
                automocker.Use(dataContext);

                var service = automocker.CreateInstance<UserTaskService>();

                var result = service.GetUserTaskById(userTaskId);

                Assert.IsNotNull(result);
            }
        }


        [TestMethod]
        public async Task CreateUserTask_ReturnValue()
        {
            var automocker = new AutoMocker();

            var userTask = new UserTask()
            {
                Name = "Create Task",
                Description = "Create new task",
                Deadline = DateTime.Now,
                ImageUrl = "",
                TaskStateId = Guid.NewGuid()      
            };

            using (var dataContext = new TaskManagementContext(TestHelper.GetTaskManagementDataContextOptions()))
            {
                automocker.Use(dataContext);

                var service = automocker.CreateInstance<UserTaskService>();

                var result = service.CreateUserTask(userTask);

                Assert.IsNotNull(result);
                Assert.AreEqual(result, userTask);

            }
        }

        [TestMethod]
        public async Task UpdateUserTask_ReturnValue()
        {
            var automocker = new AutoMocker();

            var userTask = new UserTask()
            {
                Name = "Update Task",
                Description = "Update task",
                Deadline = DateTime.Now,
                ImageUrl = "",
                TaskStateId = Guid.NewGuid()
            };

            using (var dataContext = new TaskManagementContext(TestHelper.GetTaskManagementDataContextOptions()))
            {
                dataContext.UserTasks.Add(userTask);
                await dataContext.SaveChangesAsync();
                automocker.Use(dataContext);

                var service = automocker.CreateInstance<UserTaskService>();

                userTask.Description = "Description updated";

                var result = service.UpdateUserTask(userTask);

                Assert.IsNotNull(result);
                Assert.AreEqual(result, userTask);

            }
        }

        [TestMethod]
        public async Task DeleteUserTask_ReturnValue()
        {
            var automocker = new AutoMocker();

            var userTask = new UserTask()
            {
                Name = "Create Task",
                Description = "Create new task",
                Deadline = DateTime.Now,
                ImageUrl = "",
                TaskStateId = Guid.NewGuid()
            };

            using (var dataContext = new TaskManagementContext(TestHelper.GetTaskManagementDataContextOptions()))
            {
                dataContext.UserTasks.Add(userTask);
                await dataContext.SaveChangesAsync();
                automocker.Use(dataContext);

                var availableTask = dataContext.UserTasks.FirstOrDefault(c => c.Id == userTask.Id);

                Assert.IsNotNull(availableTask);

                var service = automocker.CreateInstance<UserTaskService>();

                var result = service.DeleteUserTask(userTask.Id);

                Assert.IsNotNull(result);

                availableTask = dataContext.UserTasks.FirstOrDefault(c => c.Id == userTask.Id);

                Assert.IsNull(availableTask);

            }
        }
    }
}
