using System;
using System.Threading.Tasks;
using Kafka.Basic;

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
            Task.Run(() =>
            {
                //TODO: Use Basic Kafka to Subscribe to demo2, and update Histograms.
                using (var client = new KafkaClient("docker:2181"))
                {
                    var consumerGroup = client.Consumer("web-dash");
                    using (var instance = consumerGroup.Join())
                    {
                        instance.Subscribe("demo2")
                            .Data(message =>
                            {
                                // Do something with message...
                                _histograms.UpdateDatabaseOperation(message.Key, int.Parse(message.Value));
                            })
                            .Start()
                            .Block(); // Block the thread from disposing everything
                    }
                }

            }
                )
            ;
        }

        public void Dispose()
        {
            //TODO: Cleanup
        }
    }
}