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
            var result = new List<UnSettled>();
            result.AddRange(GetUnsettledHighWinRate());
            return result;
        }

        public IEnumerable<Settled> GetUnusualWin()
        {
            return repository.SettledRecords.Where(x => (double)x.Stake / (double)x.Win > 0.6);
        }

        private IEnumerable<UnSettled> GetUnsettledHighWinRate()
        {
            return repository.UnsettledRecords.Where(x => (double)x.Stake / (double)x.ToWin > 0.6);
        }
    }
}
