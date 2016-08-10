using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Risk.Data;
using System.Linq;

namespace Risk.Tests
{
    [TestClass]
    public class BetRepositoryTests
    {
        [TestMethod]
        public void LoadSettledTest()
        {
            var path = @"C:\Users\ZhenXin\Source\Repos\infomatrix\Risk.Tests\TestFiles\Settled.csv";
            var repo = new BetRepository(path, null);
            Assert.AreEqual(50, repo.SettledRecords.Count());
        }
    }
}
