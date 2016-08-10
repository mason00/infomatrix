using Risk.Data;
using System.Collections.Generic;

namespace Risk.Service
{
    public interface IBetService
    {
        IEnumerable<Settled> GetUnusualWin();
        IEnumerable<UnSettled> GetHighRiskBets();
        IEnumerable<UnSettled> GetUnsettledHighWinRate();
        IEnumerable<UnSettled> GetUnsettledBigWin();
    }
}
