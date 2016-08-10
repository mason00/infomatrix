using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Risk.Data;
using System.Collections.Generic;
using Risk.Service;
using System.Linq;

namespace Risk.Tests
{
    [TestClass]
    public class BetServiceTests
    {
        [TestMethod]
        public void UnusualWinTest()
        {
            var testData = new List<Settled>();
            testData.Add(new Settled() { Stake = 55, Win = 100 });
            testData.Add(new Settled() { Stake = 65, Win = 100 });

            var mockRepo = new Mock<IBetRepository>();
            mockRepo.Setup(repo => repo.SettledRecords).Returns(testData);
            var service = new BetService(mockRepo.Object);

            Assert.AreEqual(1, service.GetUnusualWin().Count());
        }

        [TestMethod]
        public void UnsettledHighRiskBets()
        {
            var testData = new List<UnSettled>();
            testData.Add(new UnSettled() { Stake = 55, ToWin = 100 });
            testData.Add(new UnSettled() { Stake = 65, ToWin = 100 });

            var mockRepo = new Mock<IBetRepository>();
            mockRepo.Setup(repo => repo.UnsettledRecords).Returns(testData);
            var service = new BetService(mockRepo.Object);

            Assert.AreEqual(1, service.GetHighRiskBets().Count());
        }
    }
}

