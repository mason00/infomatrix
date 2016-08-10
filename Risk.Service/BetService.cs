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

        public IEnumerable<Settled> GetUnusualWin()
        {
            return repository.SettledRecords
                .GroupBy(x => new { Cust = x.Customer })
                .Where(g => g.Sum(x => x.Stake) * 0.6 < g.Sum(x => x.Win))
                .Select(a => new Settled() { Customer = a.Key.Cust });
        }

        public IEnumerable<UnSettled> GetHighRiskBets()
        {
            return repository.UnsettledRecords
                .GroupBy(x => new { Cust = x.Customer })
                .Where(g => g.Sum(x => x.Stake) * 0.6 < g.Sum(x => x.ToWin))
                .Select(a => new UnSettled() { Customer = a.Key.Cust });
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
            var result = from cus in repository.UnsettledRecords
                         join c in avg
                         on cus.Customer equals c.Cust
                         where cus.Stake / rate > c.Avg
                         select new UnSettled() { Customer = cus.Customer, Event = cus.Event };

            return result;
        }

        public IEnumerable<UnSettled> GetUnsettledBigWin()
        {
            return repository.UnsettledRecords.Where(x => x.ToWin > 1000);
        }

    }
}
