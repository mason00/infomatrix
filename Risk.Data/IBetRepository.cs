using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risk.Data
{
    public interface IBetRepository
    {
        IEnumerable<Settled> SettledRecords { get; }
        IEnumerable<UnSettled> UnsettledRecords { get; }
    }
}
