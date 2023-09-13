using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Text.Json;
using Task.Data;
using Task.Data.Entities;
using Task.Models;

namespace Task.Services.TypeState.Tests
{
    [TestClass()]
    public class TypeStateServiceTests
    {

        [TestMethod()]
        public void FullTypeStateTrueTest()
        {
            // Arrange
            var seeriContext = new Mock<SeeriContext>();
            var iTypeState = new Mock<ITypeStateService>();
            var logger = new Mock<ILogger<TypeStateService>>();
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
            var service = new TypeStateService(logger.Object, seeriContext.Object);
            var result = service.FullTypeState();
            // Assert
            Assert.AreEqual(result.Status, 200);
        }

        [TestMethod()]
        public void FullTypeStateFalseTest()
        {
            //Arrange
            var seeriContext = new Mock<SeeriContext>();
            var itypeState = new Mock<ITypeStateService>();
            var logger = new Mock<ILogger<TypeStateService>>();
            var service = new TypeStateService(logger.Object, seeriContext.Object);
            itypeState.Setup(rs => rs.FullTypeState()).Returns(new ResponseGeneralModel<string> { });
            //Act
            var result = service.FullTypeState();
            //Assert
            Assert.AreEqual(result.Status, 404);
        }

        [TestMethod()]
        public void FullTypeStateFalseTest_WhenExceptionOccurs()
        {
            //Arrange
            var seeriContext = new Mock<SeeriContext>();
            var itypeState = new Mock<ITypeStateService>();
            var logger = new Mock<ILogger<TypeStateService>>();
            var service = new TypeStateService(logger.Object, seeriContext.Object);
            itypeState.Setup(rs => rs.FullTypeState()).Returns(new ResponseGeneralModel<string> { });
            //Act
            var result = service.FullTypeState();
            //Assert
            Assert.AreEqual(result.Status, 404);
            Assert.AreEqual(result.response.warning, true);
            Assert.AreEqual(result.response.success, false);

        }
    }
}