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
            testData.Add(new Settled() { Customer = 1, Event = 1, Stake = 50, Win = 0 });
            testData.Add(new Settled() { Customer = 1, Event = 1, Stake = 50, Win = 61 });

            var mockRepo = new Mock<IBetRepository>();
            mockRepo.Setup(repo => repo.SettledRecords).Returns(testData);
            var service = new BetService(mockRepo.Object);

            Assert.AreEqual(1, service.GetUnusualWin().Count());
        }

        [TestMethod]
        public void UnsettledHighRiskBets()
        {
            var testData = new List<UnSettled>();
            testData.Add(new UnSettled() { Customer = 1, Event = 1, Stake = 50, ToWin = 0 });
            testData.Add(new UnSettled() { Customer = 1, Event = 1, Stake = 50, ToWin = 61 });

            var mockRepo = new Mock<IBetRepository>();
            mockRepo.Setup(repo => repo.UnsettledRecords).Returns(testData);
            var service = new BetService(mockRepo.Object);

            Assert.AreEqual(1, service.GetHighRiskBets().Count());
        }

        [TestMethod]
        public void Get10TimesAvgBetsTest()
        {
            var historyData = new List<Settled>();
            historyData.Add(new Settled() { Customer = 1, Stake = 50 });
            historyData.Add(new Settled() { Customer = 1, Stake = 50 });
            historyData.Add(new Settled() { Customer = 2, Stake = 100 });
            historyData.Add(new Settled() { Customer = 2, Stake = 100 });

            var testData = new List<UnSettled>();
            testData.Add(new UnSettled() { Customer = 1, Stake = 510 });
            testData.Add(new UnSettled() { Customer = 2, Stake = 100 });

            var mockRepo = new Mock<IBetRepository>();
            mockRepo.Setup(repo => repo.SettledRecords).Returns(historyData);
            mockRepo.Setup(repo => repo.UnsettledRecords).Returns(testData);
            var service = new BetService(mockRepo.Object);

            Assert.AreEqual(1, service.GetUnsettledHighWinRate().Count());
        }

        [TestMethod]
        public void Get30TimesAvgBetsTest()
        {
            var historyData = new List<Settled>();
            historyData.Add(new Settled() { Customer = 1, Stake = 50 });
            historyData.Add(new Settled() { Customer = 1, Stake = 50 });
            historyData.Add(new Settled() { Customer = 2, Stake = 100 });
            historyData.Add(new Settled() { Customer = 2, Stake = 100 });

            var testData = new List<UnSettled>();
            testData.Add(new UnSettled() { Customer = 1, Stake = 1400 });
            testData.Add(new UnSettled() { Customer = 2, Stake = 3100 });

            var mockRepo = new Mock<IBetRepository>();
            mockRepo.Setup(repo => repo.SettledRecords).Returns(historyData);
            mockRepo.Setup(repo => repo.UnsettledRecords).Returns(testData);
            var service = new BetService(mockRepo.Object);

            Assert.AreEqual(1, service.GetExtremeHighWinRate().Count());
        }
    }
}

