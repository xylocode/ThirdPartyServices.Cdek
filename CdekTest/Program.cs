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

            var api = new CdekClient(httpMessageHandler: tracer);
            var req = new XyloCode.ThirdPartyServices.Cdek.Models.CalculationRequest
            {
                Date = DateOnly.FromDateTime( DateTime.Today),
                Type = 2,
                AdditionalOrderTypes = new() { 2 },
                FromLocation = new XyloCode.ThirdPartyServices.Cdek.General.Location { Address = "192012, город Санкт-Петербург, пр-кт Обуховской Обороны, д. 112 к. 2 литера И, помещ. 501" },
                ToLocation = new XyloCode.ThirdPartyServices.Cdek.General.Location { Address = "630136, Новосибирская область, г. Новосибирск, ул. Плахотного, дом № 72/1" },
                Packages = new()
                {
                    new XyloCode.ThirdPartyServices.Cdek.General.Package
                    {
                        Weight = 5000,
                        Length = 20,
                        Width = 20,
                        Height = 80,
                    }
                },
                Services = new()
                {
                    new XyloCode.ThirdPartyServices.Cdek.General.Service{ Code = XyloCode.ThirdPartyServices.Cdek.Enums.ServiceCode.INSURANCE, Parameter = "50000"}
                }
                
            };
            var result = api.MultiCalculation(req);

            // for testing (using https://api.edu.cdek.ru/ as base URI)
            //api = new CdekClient(httpMessageHandler: tracer);
            //var offices = api.GetDeliveryPoints();

            Console.Beep();
            Console.ReadLine();
        }
    }
}