using XyloCode.ThirdPartyServices.Cdek;
using XyloCode.ThirdPartyServices.Cdek.Helpers;

namespace CdekTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var clientId = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
            var clientSecret = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
            
            var tracer = new LoggingHandler(); // for trace request, needed only for debugging

            var api = new CdekClient(clientId, clientSecret, httpMessageHandler: tracer);
            var result = api.GetOrder("1234567890");

            // for testing (using https://api.edu.cdek.ru/ as base URI)
            api = new CdekClient(httpMessageHandler: tracer);
            var offices = api.GetDeliveryPoints();

            Console.Beep();
            Console.ReadLine();
        }
    }
}