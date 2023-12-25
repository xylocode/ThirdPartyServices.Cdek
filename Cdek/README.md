# CDEK API client library

An unofficial .NET full support client library for accessing the CDEK API v2.0.

- [NuGet](https://www.nuget.org/packages/XyloCode.ThirdPartyServices.Cdek) (.NET library)
- [GitHub](https://github.com/xylocode/ThirdPartyServices.Cdek) (source code)
- [Official documentation](https://api-docs.cdek.ru/33828739.html)

#### Supported Platforms

- .NET 6.0 LTS;
- .NET 7.0;
- .NET 8.0 LTS.

## CDEK

The express delivery company was founded in Novosibirsk by graduates of the Novosibirsk State University Leonid Goldort and Vyacheslav Piksayev in 2000 to transport goods from the Korzina.ru online store to the cities of Siberia and the Russian Far East.

Official website: [https://www.cdek.ru/](https://www.cdek.ru/).

## Supported Methods

Full support.

## How to use

```cs
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
            
            var tracer = new LoggingHandler(); // for tracing, needed only for debugging

            var api = new CdekClient(clientId, clientSecret, httpMessageHandler: tracer);
            var result = api.GetOrder("1234567890");

            // for testing (using https://api.edu.cdek.ru/ as base URI)
            var testApi = new CdekClient(httpMessageHandler: tracer);
            var offices = testApi.GetDeliveryPoints();

            Console.Beep();
            Console.ReadLine();
        }
    }
}
```

## License

MIT License
