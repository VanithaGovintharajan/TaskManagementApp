using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Controllers;
using TaskManagement.Models;
using TaskManagement.Services.Contracts;


namespace TaskManagement.UnitTests.Controllers
{
    [TestClass]
    public class UserTaskControllerTests
    {
        [TestMethod]
        public async Task GetUserTasks_ReturnsOk()
        {
            var automocker = new AutoMocker();
            var httpContext = new Mock<HttpContext>();
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


            //act
            var controller = automocker.CreateInstance<UserTaskController>();
            controller.ControllerContext.HttpContext = httpContext.Object;

            automocker.Setup<IUserTaskService, IEnumerable<UserTask>>(x => x.GetAllUserTasks()).Returns(userTask);

            var result = await controller.GetAllUserTasks();

            var task = result as List<UserTask>;
            // Assert
            Assert.IsNotNull(result);        
        }


        [TestMethod]
        public async Task GetUserTaskById_ReturnsOk()
        {
            var automocker = new AutoMocker();
            var httpContext = new Mock<HttpContext>();
            var userTaskId = Guid.NewGuid();
            var userTask =  new UserTask()
            {
                    Id = userTaskId,
                    Name = "Task Name",
                    Description = "Create new task",
                    Deadline = DateTime.Now,
                    ImageUrl = "",
                    TaskStateId = Guid.NewGuid()               
            };


            //act
            var controller = automocker.CreateInstance<UserTaskController>();
            controller.ControllerContext.HttpContext = httpContext.Object;

            automocker.Setup<IUserTaskService, UserTask>(x => x.GetUserTaskById(It.IsAny<Guid>())).Returns(userTask);

            var result = await controller.GetUserTaskById(userTaskId);

            var task = (result as OkObjectResult).Value as UserTask;

            // Assert
            Assert.IsNotNull(result);            
        }


        [TestMethod]
        public async Task CreateTask_ReturnsOk()
        {
            var automocker = new AutoMocker();
            var httpContext = new Mock<HttpContext>();
            var userTask = new UserTask()
            {
                Id = Guid.NewGuid(),
                Name = "Task Name",
                Description = "Create new task",
                Deadline = DateTime.Now,
                ImageUrl = "",
                TaskStateId = Guid.NewGuid()
            };

            //act
            var controller = automocker.CreateInstance<UserTaskController>();
            controller.ControllerContext.HttpContext = httpContext.Object;

            automocker.Setup<IUserTaskService, UserTask>(x => x.CreateUserTask(It.IsAny<UserTask>())).Returns(userTask);

            var result = await controller.CreateUserTask(userTask);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CreatedResult));
        }

        [TestMethod]
        public async Task CreateUserTask_ReturnsBadRequest()
        {
            var automocker = new AutoMocker();
            UserTask userTask = null;

            //act
            var controller = automocker.CreateInstance<UserTaskController>();

            var result = await controller.CreateUserTask(userTask);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task UpdateUserTask_ReturnsOk()
        {
            var automocker = new AutoMocker();
            var userTask = new UserTask()
            {
                Id = Guid.NewGuid(),
                Name = "Task Name",
                Description = "Create new task",
                Deadline = DateTime.Now,
                ImageUrl = "",
                TaskStateId = Guid.NewGuid()
            };

            //act
            var controller = automocker.CreateInstance<UserTaskController>();

            automocker.Setup<IUserTaskService, UserTask>(x => x.CreateUserTask(It.IsAny<UserTask>())).Returns(userTask);

            await controller.CreateUserTask(userTask);

            userTask.Description = "Description Updated";

            automocker.Setup<IUserTaskService, UserTask>(x => x.UpdateUserTask(It.IsAny<UserTask>())).Returns(userTask);

            var result = await controller.UpdateUserTask(userTask);

            UserTask updateUserTask = (UserTask)(result as OkObjectResult).Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userTask, updateUserTask);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task UpdateUserTask_ReturnsBadRequest()
        {
            var automocker = new AutoMocker();
            UserTask userTask = null;

            //act
            var controller = automocker.CreateInstance<UserTaskController>();

            var result = await controller.UpdateUserTask(userTask);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task DeleteUserTask_ReturnsOk()
        {
            var automocker = new AutoMocker();
            var userTask = new UserTask()
            {
                Id = Guid.NewGuid(),
                Name = "Task Name",
                Description = "Create new task",
                Deadline = DateTime.Now,
                ImageUrl = "",
                TaskStateId = Guid.NewGuid()
            };

            //act
            var controller = automocker.CreateInstance<UserTaskController>();

            automocker.Setup<IUserTaskService, UserTask>(x => x.CreateUserTask(It.IsAny<UserTask>())).Returns(userTask);

            await controller.CreateUserTask(userTask);

            userTask.Description = "Description Updated";

            automocker.Setup<IUserTaskService, bool>(x => x.DeleteUserTask(It.IsAny<Guid>())).Returns(true);

            var result = await controller.DeleteUserTask(userTask.Id);

            var res = (result as OkObjectResult).Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(res, "Task deleted");
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task DeleteUserTask_ReturnsBadRequest()
        {
            var automocker = new AutoMocker();
            UserTask userTask = new UserTask();

            //act
            var controller = automocker.CreateInstance<UserTaskController>();

            var result = await controller.DeleteUserTask(userTask.Id);
            // Assert

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

    }
}
