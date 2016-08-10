using Risk.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risk.Service
{
    public class BetService : IBetService
    {
        IBetRepository repository;

        public BetService(IBetRepository repo)
        {
            repository = repo;
        }

        public IEnumerable<UnSettled> GetHighRiskBets()
        {
            return repository.UnsettledRecords.Where(x => x.ToWin > 0 && (double)x.Stake / (double)x.ToWin > 0.6);
        }

        public IEnumerable<UnSettled> GetUnsettledBigWin()
        {
            return repository.UnsettledRecords.Where(x => x.ToWin > 1000);
        }

        public IEnumerable<Settled> GetUnusualWin()
        {
            return repository.SettledRecords.Where(x => x.Win > 0 && (double)x.Stake / (double)x.Win > 0.6);
        }

        public IEnumerable<UnSettled> GetUnsettledHighWinRate()
        {
            return GetRiskBetByRate(10);
        }

        public IEnumerable<UnSettled> GetExtremeHighWinRate()
        {
            return GetRiskBetByRate(30);
        }

        private IEnumerable<UnSettled> GetRiskBetByRate(int rate = 10)
        {
            var avg = repository.SettledRecords
                .GroupBy(x => new { Cust = x.Customer })
                .Select(g => new { Avg = g.Average(x => x.Stake), Cust = g.Key.Cust });
            var result = from cus in avg
                         join c in repository.UnsettledRecords
                         on cus.Cust equals c.Customer
                         where c.Stake / rate > cus.Avg
                         select c;

            return result;
        }
    }
}
