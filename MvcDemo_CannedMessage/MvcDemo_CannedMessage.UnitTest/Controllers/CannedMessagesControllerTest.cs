using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using MvcDemo_CannedMessage.Service.AppServices;
using MvcDemo_CannedMessage.Entity;
using MvcDemo_CannedMessage.Controllers;
using System.Web.Mvc;
using Xunit;

namespace MvcDemo_CannedMessage.UnitTest.Controllers
{
    public class CannedMessagesControllerTest
    {
        [Fact]
        public void ShouldGetCannedMessage()
        {
            var testEntity = new CannedMessage
            {
                Id = 1,
                Message = "Test Message",
                Name = "Test Name",
                Shortcuts = "Test Shortcuts"
            };
            var appService = new Mock<ICannedMessageAppService>();
            appService
                .Setup(t => t.Find(It.Is<int>(p => p == testEntity.Id)))
                .Returns(testEntity);

            var controller = new CannedMessagesController(appService.Object);
            var result = controller.Details(testEntity.Id) as ViewResult;

            Assert.Equal(testEntity, result.Model);
        }

        [Fact]
        public void ShouldGetNotFoundStatus_WhenMessageNotFound()
        {
            var appService = new Mock<ICannedMessageAppService>();
            appService
                .Setup(t => t.Find(It.IsAny<int>()))
                .Returns<CannedMessage>(null);

            var controller = new CannedMessagesController(appService.Object);
            var result = controller.Details(1) as HttpNotFoundResult;

            Assert.Equal(404, result.StatusCode);
        }
    }
}
