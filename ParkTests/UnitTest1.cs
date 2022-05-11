using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ParkoMatikDB.Models;
using System;

namespace ParkTests
{
    [TestClass]
    public class UnitTest1
    {
        Mock<DbSet<Pass>> mockSet;
        Mock<ParkingContext> mockContext;
        ParkingHelper service;

        [TestMethod]
        public void CreatePassSavesAPassViaContext()
        {
            mockSet = new Mock<DbSet<Pass>>();

            mockContext = new Mock<ParkingContext>();
            mockContext.Setup(m => m.Passes).Returns(mockSet.Object);

            service = new ParkingHelper(mockContext.Object);
            service.CreatePass("bill", true, 3);

            mockSet.Verify(m => m.Add(It.IsAny<Pass>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void CreatePassThrowExceptionIfPurchaserNotBetween3And20Chars()
        {
            mockSet = new Mock<DbSet<Pass>>();

            mockContext = new Mock<ParkingContext>();
            mockContext.Setup(m => m.Passes).Returns(mockSet.Object);

            service = new ParkingHelper(mockContext.Object);

            Assert.ThrowsException<Exception>(() => service.CreatePass("someguywhohasareallylongnameandismorethantwentychars", true, 3));
        }

        [TestMethod]
        public void CreatePassThrowExceptionIfCapacityisZeroOrLess()
        {
            mockSet = new Mock<DbSet<Pass>>();

            mockContext = new Mock<ParkingContext>();
            mockContext.Setup(m => m.Passes).Returns(mockSet.Object);

            service = new ParkingHelper(mockContext.Object);

            Assert.ThrowsException<Exception>(() => service.CreatePass("steve", true, 0));
        }
    }
}