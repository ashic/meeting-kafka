using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;

namespace WebDash.Models
{
    public class Histograms
    {
        public static Histograms Instance = new Histograms();

        private readonly object _lock = new object();
        private readonly Dictionary<string, int> _hists = new Dictionary<string, int>();

        private Histograms()
        {
            _hists.Add("a", 22);
            _hists.Add("b", 21);
        }

        public void UpdateDatabaseOperation(string key, int value)
        {
            lock (_lock)
            {
                _hists[key] = value;
            }
        }

        public List<HistogramEntry> GetValues()
        {
            lock(_lock)
                return _hists.Select(x => new HistogramEntry(x.Key, x.Value)).ToList();
        }
    }

    public class HistogramEntry
    {
        public string Key { get; private set; }
        public int Value { get; private set; }

        public HistogramEntry(string key, int value)
        {
            Key = key;
            Value = value;
        }
    }
}