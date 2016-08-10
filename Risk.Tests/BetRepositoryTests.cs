using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Risk.Data;
using System.Linq;
using System.Diagnostics;

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
            var repo = new BetRepository();
            Assert.AreEqual(50, repo.SettledRecords.Count());
        }

        [TestMethod]
        public void LoadUnSettledTest()
        {
            var repo = new BetRepository();
            Assert.AreEqual(22, repo.UnsettledRecords.Count());
        }

        [TestMethod]
        public void Group()
        {
            var repo = new BetRepository();
            var result = from c in repo.SettledRecords
                         group c by c.Customer into g
                         orderby g.Key descending
                         select new { Cust = g.Key, TotalStake = g.Sum(x => x.Stake) };

            foreach (var g in result)
            { 
                Debug.WriteLine(string.Format("{0} {1}", g.Cust, g.TotalStake));
            }
        }

        [TestMethod]
        public void Join()
        {
            var repo = new BetRepository();
            var result = from c in repo.SettledRecords
                         join u in repo.UnsettledRecords
                         on c.Customer equals u.Customer
                         select new { Cust = c.Customer, Win = c.Win, ToWin = u.ToWin };

            foreach (var g in result)
            {
                Debug.WriteLine(string.Format("{0} {1} {2}", g.Cust, g.Win, g.ToWin));
            }
        }
    }
}
