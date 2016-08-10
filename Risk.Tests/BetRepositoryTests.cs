using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Risk.Data;
using System.Linq;

namespace Risk.Tests
{
    [TestClass]
    public class BetRepositoryTests
    {
        string settledPath = @"C:\Users\ZhenXin\Source\Repos\infomatrix\Risk.Tests\TestFiles\Settled.csv";
        string unsettledPath = @"C:\Users\ZhenXin\Source\Repos\infomatrix\Risk.Tests\TestFiles\Unsettled.csv";

        [TestMethod]
        public void LoadSettledTest()
        {
            var repo = new BetRepository(settledPath, unsettledPath);
            Assert.AreEqual(50, repo.SettledRecords.Count());
        }

        [TestMethod]
        public void LoadUnSettledTest()
        {
            var repo = new BetRepository(settledPath, unsettledPath);
            Assert.AreEqual(22, repo.UnsettledRecords.Count());
        }
    }
}
