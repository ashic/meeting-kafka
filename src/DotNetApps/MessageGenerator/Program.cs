using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Kafka.Basic;

namespace MessageGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random(DateTime.Now.Millisecond);

            //TODO: Setup consumer...need topic (see below)
            {
                while (true)
                {
                    var batch = Enumerable.Range(1, 10).Select(x =>
                        new Message()
                        {
                            Key = "a",
                            Value = (DateTime.Now.Millisecond + random.Next()).ToString()
                        }).ToArray();

                    //TODO: Uncomment
                    //topic.Send(batch);
                    Console.WriteLine("Sending batch....");
                   Thread.Sleep(2000);
                }
            }
        }
    }
}
