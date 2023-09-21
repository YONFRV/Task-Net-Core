using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Task.Data;
using Task.Data.Entities;
using Task.Models;
using Task.Services.TypeStateService.GetAllTypeStateService;

namespace Task.Services.TypeState.Tests
{
    [TestClass()]
    public class GetAllTypeStateServiceTests
    {

        [TestMethod()]
        public void GetAllTypeStateTrueTest()
        {
            // Arrange
            var seeriContext = new Mock<SeeriContext>();
            var iTypeState = new Mock<IGetAllTypeStateService>();
            Mock<ILogger<GetAllTypeStateService>> logger = new Mock<ILogger<GetAllTypeStateService>>();
            List<TypeStateEntity> listTypeState = new List<TypeStateEntity>
                {
                    new TypeStateEntity { TypeStateId = 1, Name = "Prueba1", CreateDate = DateTime.Now },
                    new TypeStateEntity { TypeStateId = 2, Name = "Prueba2", CreateDate = DateTime.Now }
                };
            var mockSet = new Mock<DbSet<TypeStateEntity>>();
            mockSet.As<IQueryable<TypeStateEntity>>().Setup(m => m.Provider).Returns(listTypeState.AsQueryable().Provider);
            mockSet.As<IQueryable<TypeStateEntity>>().Setup(m => m.Expression).Returns(listTypeState.AsQueryable().Expression);
            mockSet.As<IQueryable<TypeStateEntity>>().Setup(m => m.ElementType).Returns(listTypeState.AsQueryable().ElementType);
            mockSet.As<IQueryable<TypeStateEntity>>().Setup(m => m.GetEnumerator()).Returns(listTypeState.AsQueryable().GetEnumerator());
            seeriContext.Setup(c => c.TypeStates).Returns(mockSet.Object);
            // Act
            var service = new GetAllTypeStateService(logger.Object, seeriContext.Object);
            var result = service.GetAllTypeState();
            // Assert
            Assert.AreEqual(result.Status, 200);
        }

        [TestMethod()]
        public void GetAllTypeStateFalseTest()
        {
            //Arrange
            var seeriContext = new Mock<SeeriContext>();
            var itypeState = new Mock<IGetAllTypeStateService>();
            var logger = new Mock<ILogger<GetAllTypeStateService>>();
            var service = new GetAllTypeStateService(logger.Object, seeriContext.Object);
            itypeState.Setup(rs => rs.GetAllTypeState()).Returns(new ResponseGeneralModel<string> { });
            //Act
            var result = service.GetAllTypeState();
            //Assert
            Assert.AreEqual(result.Status, 404);
        }

        [TestMethod()]
        public void GetAllTypeStateFalseTest_WhenExceptionOccurs()
        {
            //Arrange
            var seeriContext = new Mock<SeeriContext>();
            var itypeState = new Mock<IGetAllTypeStateService>();
            var logger = new Mock<ILogger<GetAllTypeStateService>>();
            var service = new GetAllTypeStateService(logger.Object, seeriContext.Object);
            itypeState.Setup(rs => rs.GetAllTypeState()).Returns(new ResponseGeneralModel<string> { });
            //Act
            var result = service.GetAllTypeState();
            //Assert
            Assert.AreEqual(result.Status, 404);
            Assert.AreEqual(result.response.warning, true);
            Assert.AreEqual(result.response.success, false);

        }
    }
}