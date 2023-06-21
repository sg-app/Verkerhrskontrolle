using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verkehrskontrolle.Controllers;
using Verkehrskontrolle.Data;
using Verkehrskontrolle.Models;

namespace VerkehrskontrolleTests.Controllers
{
    public class HalterControllerTests
    {
        [Test]
        public async Task GetAll_ShouldReturnsListOfHalter()
        {
            // Arrange
            var list = Helpers.HelperData.HalterList();
            
            var ctxMock = new Mock<VerkehrskontrolleDbContext>();
            ctxMock.Setup(f => f.Halter).ReturnsDbSet(list);

            var controller = new HalterController(ctxMock.Object);
            
            // Act
            var actual = await controller.GetAll();
            
            // Assert
            actual.Value.Should().BeEquivalentTo(list);
        }

        [Test]
        public async Task Get_ShouldReturnsOneHalter()
        {
            // Arrange
            var list = Helpers.HelperData.HalterList();

            var halterSetMock = new Mock<DbSet<Halter>>();
            halterSetMock.Setup(x => x.FindAsync(It.IsAny<int>())).ReturnsAsync(list[1]);
           
            var ctxMock = new Mock<VerkehrskontrolleDbContext>();
            ctxMock.SetupGet(f => f.Halter).Returns(halterSetMock.Object);

            var controller = new HalterController(ctxMock.Object);

            // Act
            var actual = await controller.Get(1);

            // Assert
            ((OkObjectResult)actual.Result).Value.Should().Be(list[1]);
        }

        [Test]
        public async Task Get_ShouldReturnsNoContent()
        {
            // Arrange
            var list = Helpers.HelperData.HalterList();

            var ctxMock = new Mock<VerkehrskontrolleDbContext>();
            ctxMock.SetupGet(f => f.Halter).ReturnsDbSet(list);

            var controller = new HalterController(ctxMock.Object);

            // Act
            var actual = await controller.Get(-1);

            // Assert
            actual.Result.Should().BeOfType<NoContentResult>();
        }


    }
}
