using System;
using System.Threading.Tasks;
using XyloCode.ThirdPartyServices.Cdek;
using XyloCode.ThirdPartyServices.Cdek.Helpers;

namespace CdekTestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () => await Test());
            Console.ReadLine();
        }

        private static async Task Test()
        {
            //var clientId = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
            //var clientSecret = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";

            var tracer = new LoggingHandler(); // for tracing, needed only for debugging

            //var api = new CdekClient(clientId, clientSecret, httpMessageHandler: tracer);
            //var result = api.GetOrder("1234567890");

            // for testing (using https://api.edu.cdek.ru/ as base URI)
            var testApi = new CdekClient(httpMessageHandler: tracer);
            var offices = testApi.GetDeliveryPointsAsync();

            await foreach (var office in offices)
            {
                Console.WriteLine("{0}:\t{1}", office.Code, office.Location.Address);
            }
            Console.Beep();
        }
    }
}
