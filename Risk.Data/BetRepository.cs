using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risk.Data
{
    public class BetRepository : IBetRepository
    {
        const string settledPath = @"C:\Users\ZhenXin\Source\Repos\infomatrix\Risk.Tests\TestFiles\Settled.csv";
        const string unsettledPath = @"C:\Users\ZhenXin\Source\Repos\infomatrix\Risk.Tests\TestFiles\Unsettled.csv";

        IEnumerable<Settled> settledRecords;
        IEnumerable<UnSettled> unsettledRecords;

        public IEnumerable<Settled> SettledRecords
        {
            get
            {
                if (settledRecords == null)
                {
                    Load();
                }
                return settledRecords;
            }
        }

        public IEnumerable<UnSettled> UnsettledRecords
        {
            get
            {
                if (unsettledRecords == null)
                {
                    Load();
                }
                return unsettledRecords;
            }
        }

        private void Load()
        {
            settledRecords = LoadSettledFromFile(settledPath);
            unsettledRecords = LoadUnsettledFromFile(unsettledPath);
        }

        private IEnumerable<UnSettled> LoadUnsettledFromFile(string unsettledFile)
        {
            var result = new List<UnSettled>();
            var reader = new StreamReader(File.OpenRead(unsettledFile));
            bool isHeader = true;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (isHeader)
                {
                    isHeader = false;
                    continue;
                }

                var values = line.Split(',');

                var settled = new UnSettled();
                settled.Customer = Convert.ToInt16(values[0]);
                settled.Event = Convert.ToInt16(values[1]);
                settled.Participant = Convert.ToInt16(values[2]);
                settled.Stake = Convert.ToInt16(values[3]);
                settled.ToWin = Convert.ToInt16(values[4]);

                result.Add(settled);
            }
            return result;
        }

        private IEnumerable<Settled> LoadSettledFromFile(string settledFile)
        {
            var result = new List<Settled>();
            var reader = new StreamReader(File.OpenRead(settledFile));
            bool isHeader = true;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (isHeader)
                {
                    isHeader = false;
                    continue;
                }

                var values = line.Split(',');

                var settled = new Settled();
                settled.Customer = Convert.ToInt16(values[0]);
                settled.Event = Convert.ToInt16(values[1]);
                settled.Participant = Convert.ToInt16(values[2]);
                settled.Stake = Convert.ToInt16(values[3]);
                settled.Win = Convert.ToInt16(values[4]);

                result.Add(settled);
            }
            return result;
        }
    }
}
