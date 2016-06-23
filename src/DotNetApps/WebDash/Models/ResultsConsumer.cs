using System;

namespace WebDash.Models
{
    public class ResultsConsumer : IDisposable 
    {
        private readonly Histograms _histograms;

        public ResultsConsumer(Histograms histograms)
        {
            _histograms = histograms;
        }

        public void Start()
        {
            //TODO: Use Basic Kafka to Subscribe to demo2, and update Histograms.
        }

        public void Dispose()
        {
            //TODO: Cleanup
        }
    }
}