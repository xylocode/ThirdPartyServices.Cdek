using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading;
using XyloCode.ThirdPartyServices.Cdek.Enums;
using XyloCode.ThirdPartyServices.Cdek.General;
using XyloCode.ThirdPartyServices.Cdek.Helpers;
using XyloCode.ThirdPartyServices.Cdek.Models;
using XyloCode.ThirdPartyServices.Cdek.Requests;
using XyloCode.ThirdPartyServices.Cdek.Responses;

namespace XyloCode.ThirdPartyServices.Cdek
{
    public class CdekClient
    {
        const string baseUri = "https://api.cdek.ru";
        const string testBaseUri = "https://api.edu.cdek.ru";
        const string testClientId = "EMscd6r9JnFiQ3bLoyjJY6eM78JrJceI";
        const string testClientSecret = "PjLZkKBHEiLK3YsjtNrt3TGNG0ahs3kG";

        private readonly bool isTest = false;
        private readonly string clientId;
        private readonly string clientSecret;
        private long expiresIn = 0;

        private readonly HttpClient httpClient;
        private readonly JsonSerializerOptions jso;
        private readonly QueryStringSerializer qss;

        public CdekClient(HttpMessageHandler httpMessageHandler = null, bool disposeHandler = true) :
            this(testClientId, testClientSecret, testBaseUri, httpMessageHandler, disposeHandler)
        {
            isTest = true;
        }

        public CdekClient(
            string clientId,
            string clientSecret,
            string baseUri = baseUri,
            HttpMessageHandler httpMessageHandler = null,
            bool disposeHandler = true
            )
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;

            if (httpMessageHandler is null)
                httpClient = new HttpClient();
            else
                httpClient = new HttpClient(httpMessageHandler, disposeHandler);

            httpClient.BaseAddress = new Uri(baseUri);

            jso = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = new JsonSnakeCaseNamingPolicy(),
            };
            jso.Converters.Add(new JsonStringEnumConverter());
            jso.Converters.Add(new DateTimeOffsetConverter());

            qss = new QueryStringSerializer
            {
                NameConverter = new JsonSnakeCaseNamingPolicy().ConvertName,
                ArraySerializationMode = QueryStringSerializer.ArraySerializationModeEnum.NameOnly,
            };
        }

        public bool IsTest => isTest;

        private enum RequestMethod : byte
        {
            GET,
            POST,
            PUT,
            DELETE,
            PATCH,
        }

        private TRes Send<TRes>(RequestMethod method, string path)
        {
            if (DateTime.Now.Ticks > expiresIn)
                Auth();
            Thread.Sleep(1000);


            HttpResponseMessage res;
            switch (method)
            {
                case RequestMethod.GET:
                    res = httpClient
                        .GetAsync(path)
                        .Result;
                    break;

                case RequestMethod.DELETE:
                    res = httpClient
                        .DeleteAsync(path)
                        .Result;
                    break;

                default:
                    throw new NotSupportedException();
            }

            if (!res.IsSuccessStatusCode)
                throw new Exception(res.ReasonPhrase);

            return res
                .Content
                .ReadFromJsonAsync<TRes>(jso)
                .Result;
        }

        private TRes Send<TRes, TReq>(RequestMethod method, string path, TReq req)
        {
            if (DateTime.Now.Ticks > expiresIn)
                Auth();
            Thread.Sleep(1000);

            if (method == RequestMethod.GET || method == RequestMethod.DELETE)
                path = string.Concat(path, "?", qss.Serialize(req));

            HttpResponseMessage res;

            switch (method)
            {
                case RequestMethod.GET:
                    res = httpClient
                        .GetAsync(path)
                        .Result;
                    break;

                case RequestMethod.POST:
                    res = httpClient
                        .PostAsJsonAsync(path, req, jso)
                        .Result;
                    break;

                case RequestMethod.PUT:
                    res = httpClient
                        .PutAsJsonAsync(path, req, jso)
                        .Result;
                    break;

                case RequestMethod.DELETE:
                    res = httpClient
                        .DeleteAsync(path)
                        .Result;
                    break;

                case RequestMethod.PATCH:
#if NET6_0
                    res = httpClient.PatchAsync(path, JsonContent.Create(req, options: jso)).Result;
#endif
#if NET7_0
                    res = httpClient.PatchAsJsonAsync(path, req, jso).Result;
#endif
                    break;

                default:
                    throw new NotSupportedException();
            }

            return res
                .Content
                .ReadFromJsonAsync<TRes>(jso)
                .Result;
        }


        private void Auth()
        {
            var oauth = new Dictionary<string, string>
            {
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "grant_type", "client_credentials" }
            };
            
            var form = new FormUrlEncodedContent(oauth);

            httpClient.DefaultRequestHeaders.Authorization = null;
            
            var res = httpClient
                .PostAsync("/v2/oauth/token?parameters", form)
                .Result;

            if (!res.IsSuccessStatusCode)
                throw new Exception(res.ReasonPhrase);

            var auth = res
                .Content
                .ReadFromJsonAsync<Models.Authorization>(jso)
                .Result;

            httpClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", auth.AccessToken);
            expiresIn = DateTime.Now.AddSeconds(auth.ExpiresIn).Ticks;
        }


        /// <summary>
        /// Метод предназначен для создания в ИС СДЭК заказа на доставку товаров до покупателей.
        /// Выделяется 2 типа заказов:
        /// “интернет-магазин” - может быть только у клиента с типом договора “Интернет-магазин”;
        /// “доставка” может быть создан любым клиентом с договором(но доступны тарифы только для обычной доставки).
        /// </summary>
        public EntityResponse<Entity> NewOrder(Order order) =>
            Send<EntityResponse<Entity>, Order>(RequestMethod.POST, "/v2/orders", order);

        /// <summary>
        /// Метод предназначен для получения детальной информации по заданному заказу.
        /// Есть возможность получить информацию в том числе о заказах, которые были созданы через другие каналы(личный кабинет, протокол v.1.5 и др.).
        /// Но только по тем, которые были созданы после выдачи индивидуальных ключей доступа.
        /// <param name="uuid">Идентификатор заказа в ИС СДЭК, по которому необходима информация</param>
        public EntityResponse<Order> GetOrder(Guid uuid) =>
            Send<EntityResponse<Order>>(RequestMethod.GET, $"/v2/orders/{uuid}");

        /// <summary>
        /// Метод предназначен для получения детальной информации по заданному заказу.
        /// Есть возможность получить информацию в том числе о заказах, которые были созданы через другие каналы(личный кабинет, протокол v.1.5 и др.).
        /// Но только по тем, которые были созданы после выдачи индивидуальных ключей доступа.
        /// <param name="cdekNumber">Номер заказа СДЭК, по которому необходима информация</param>
        public EntityResponse<Order> GetOrder(string cdekNumber) =>
            Send<EntityResponse<Order>>(RequestMethod.GET, $"/v2/orders?cdek_number={cdekNumber}");

        /// <summary>
        /// Метод предназначен для получения детальной информации по заданному заказу.
        /// Есть возможность получить информацию в том числе о заказах, которые были созданы через другие каналы(личный кабинет, протокол v.1.5 и др.).
        /// Но только по тем, которые были созданы после выдачи индивидуальных ключей доступа.
        /// </summary>
        /// <param name="number">Номер заказа в ИС Клиента, по которому необходима информация</param>
        public EntityResponse<Order> GetOrderByInternalNumber(string number) =>
            Send<EntityResponse<Order>>(RequestMethod.GET, $"/v2/orders?im_number={number}");


        /// <summary>
        /// Метод используется для изменения созданного ранее заказа.
        /// Условием возможности изменения заказа является отсутствие движения груза на складе СДЭК (т.е.статус заказа «Создан»).
        /// </summary>
        public EntityResponse<Entity> EditOrder(Order order) =>
            Send<EntityResponse<Entity>, Order>(RequestMethod.PATCH, "/v2/orders", order);


        /// <summary>
        /// Метод предназначен для удаления заказа.
        /// Условием возможности удаления заказа является отсутствие движения груза на складе СДЭК (статус заказа «Создан»).
        /// </summary>
        /// <param name="uuid">Идентификатор заказа в ИС СДЭК, который необходимо удалить</param>
        public EntityResponse<Entity> DeleteOrder(Guid uuid) =>
            Send<EntityResponse<Entity>>(RequestMethod.DELETE, $"/v2/orders/{uuid}");


        /// <summary>
        /// Метод предназначен для регистрации отказа по заказу и дальнейшего возврата данного заказа в интернет-магазин.
        /// После успешной регистрации отказа статус заказа переходит в "Не вручен" (код NOT_DELIVERED) с дополнительным статусом "Возврат, отказ от получения: Без объяснения" (код 11).
        /// Заказ может быть отменен в любом статусе, пока не установлен статус "Вручен" или "Не вручен".
        /// </summary>
        /// <param name="uuid">Идентификатор заказа в ИС СДЭК, по которому необходимо зарегистрировать отказ</param>
        public EntityResponse<Entity> Refusal(Guid uuid) =>
            Send<EntityResponse<Entity>>(RequestMethod.POST, $"/v2/orders/{uuid}/refusal");


        /// <summary>
        /// Метод позволяет осуществить вызов курьера для забора груза со склада ИМ с последующей доставкой до склада СДЭК.
        /// Рекомендуемый минимальный диапазон времени для приезда курьера не менее 3х часов.
        /// </summary>
        public EntityResponse<Entity> NewPickup(Pickup pickup) =>
            Send<EntityResponse<Entity>, Pickup>(RequestMethod.POST, "/v2/intakes", pickup);


        /// <summary>
        /// Метод предназначен для получения информации по заявке на вызов курьера.
        /// </summary>
        /// <param name="uuid">Идентификатор заявки в ИС СДЭК, по которому необходима информация</param>
        public EntityResponse<Pickup> GetPickup(Guid uuid) =>
            Send<EntityResponse<Pickup>>(RequestMethod.GET, $"/v2/intakes/{uuid}");


        /// <summary>
        /// Метод предназначен для удаление заявки на вызов курьера.
        /// </summary>
        /// <param name="uuid">Идентификатор заявки в ИС СДЭК, которую необходимо удалить</param>
        public EntityResponse<Entity> DeletePickup(Guid uuid) =>
            Send<EntityResponse<Entity>>(RequestMethod.DELETE, $"/v2/intakes/{uuid}");


        /// <summary>
        /// Метод используется для формирования квитанции в формате pdf к заказу/заказам.
        /// </summary>
        public EntityResponse<Entity> WaybillRequest(WaybillRequest req) =>
            Send<EntityResponse<Entity>, WaybillRequest>(RequestMethod.POST, "/v2/print/orders", req);


        /// <summary>
        /// Метод используется для получения ссылки на квитанцию в формате pdf к заказу/заказам.
        /// </summary>
        /// <param name="uuid">Идентификатор квитанции, ссылку на которую необходимо получить</param>
        public EntityResponse<Waybill> GetWaybill(Guid uuid) =>
            Send<EntityResponse<Waybill>>(RequestMethod.GET, $"/v2/print/orders/{uuid}");


        /// <summary>
        /// Метод используется для формирования ШК места в формате pdf к заказу/заказам.
        /// Во избежание перегрузки платформы нельзя передавать более 100 номеров заказов в одном запросе.
        /// </summary>
        public EntityResponse<Entity> BarcodeRequest(BarcodeRequest req) =>
            Send<EntityResponse<Entity>, BarcodeRequest>(RequestMethod.POST, "/v2/print/barcodes", req);


        /// <summary>
        /// Метод используется для получения ШК места в формате pdf к заказу/заказам.
        /// </summary>
        /// <param name="uuid">Идентификатор ШК места, ссылку на который необходимо получить</param>
        public EntityResponse<Barcode> GetBarcode(Guid uuid) =>
            Send<EntityResponse<Barcode>>(RequestMethod.GET, $"/v2/print/barcodes/{uuid}");


        /// <summary>
        /// Метод позволяет фиксировать оговоренные с клиентом дату и время доставки (приезда курьера), а так же изменять адрес доставки.
        /// </summary>
        public EntityResponse<Entity> NewDelivery(Delivery delivery) =>
            Send<EntityResponse<Entity>, Delivery>(RequestMethod.POST, "/v2/delivery", delivery);


        /// <summary>
        /// Метод используется для получения информации об оговоренных с клиентом дате и времени доставки (приезда курьера), а так же возможном новом адресе доставки.
        /// </summary>
        /// <param name="uuid">Идентификатор договоренности о доставке в ИС СДЭК.</param>
        public EntityResponse<Delivery> GetDelivery(Guid uuid) =>
            Send<EntityResponse<Delivery>>(RequestMethod.GET, $"/v2/delivery/{uuid}");

        /// <summary>
        /// Метод предназначен для регистрации преалерта (реестра заказов, которые клиент собирается передать на склад СДЭК для дальнейшей доставки).
        /// </summary>
        public EntityResponse<Entity> NewPrealert(Prealert prealert) =>
            Send<EntityResponse<Entity>, Prealert>(RequestMethod.POST, "/v2/prealert", prealert);


        /// <summary>
        /// Метод предназначен для получения информации по заданному преалерту.
        /// </summary>
        /// <param name="uuid">Идентификатор преалерта в ИС СДЭК, по которому необходима информация</param>
        public EntityResponse<Prealert> GetPrealert(Guid uuid) =>
            Send<EntityResponse<Prealert>>(RequestMethod.GET, $"/v2/prealert/{uuid}");


        /// <summary>
        /// Метод используется для получения информации о паспортных данных (сообщает о готовности передавать заказы на таможню) по международным заказу/заказам.
        /// </summary>
        public OrdersResponse<PassportStatus> GetPassportInfo(PassportRequest req) =>
            Send<OrdersResponse<PassportStatus>, PassportRequest>(RequestMethod.GET, $"/v2/passport", req);


        /// <summary>
        /// Метод используется для получения информации о чеке по заказу или за выбранный день.
        /// </summary>
        /// <param name="orderUuid">Идентификатор заказа в ИС СДЭК, по которому необходимо вернуть данные по чеку</param>
        /// <param name="cdekNumber">Номер заказа СДЭК, по которому необходимо вернуть данные по чеку</param>
        /// <param name="date">Дата, за которую необходимо вернуть данные по чекам</param>
        public ReceiptResponse GetReceipt(Guid? orderUuid = null, string cdekNumber = null, DateOnly? date = null)
        {
            var req = new ReceiptRequest
            {
                OrderUuid = orderUuid,
                CdekNumber = cdekNumber,
                Date = date
            };
            return Send<ReceiptResponse, ReceiptRequest>(RequestMethod.GET, $"/v2/check", req);
        }


        /// <summary>
        /// Метод предназначен для получения информации о реестрах наложенных платежей, по которым клиенту был переведен наложенный платеж в заданную клиентом дату.
        /// </summary>
        /// <param name="date">Дата, за которую необходимо вернуть реестры наложенных платежей, по которым был переведен наложенный платеж</param>
        public CashOnDeliveryRegisterResponse GetCashOnDeliveryRegister(DateOnly date)
        {
            var dateStr = date.ToString("yyyy-MM-dd");
            return Send<CashOnDeliveryRegisterResponse>(RequestMethod.GET, $"/v2/registries?date={dateStr}");
        }


        /// <summary>
        /// Метод предназначен для получения информации о заказах, по которым был переведен наложенный платеж интернет-магазину в заданную дату.
        /// </summary>
        /// <param name="date">Дата, за которую необходимо вернуть список заказов, по которым был переведен наложенный платеж</param>
        public OrdersResponse<OrderId2> GetWireTransfer(DateOnly date)
        {
            var dateStr = date.ToString("yyyy-MM-dd");
            return Send<OrdersResponse<OrderId2>>(RequestMethod.GET, $"/v2/payment?date={dateStr}");
        }


        /// <summary>
        /// Предназначены для отправки на URL клиента событий:
        /// изменении статуса заказа;
        /// готовности печатной формы.
        /// </summary>
        /// <param name="type">Тип события</param>
        /// <param name="url">URL, на который клиент хочет получать вебхуки</param>
        public EntityResponse<Entity> NewWebhook(WebhookType type, string url)
        {
            if (isTest)
                throw new NotImplementedException();

            var req = new WebhookRequest { Type = type, Url = url };
            return Send<EntityResponse<Entity>, WebhookRequest>(RequestMethod.POST, "/v2/webhooks", req);
        }


        /// <summary>
        /// Метод предназначен для получения списка действующих офисов СДЭК.
        /// </summary>
        public IEnumerable<DeliveryPoint> GetDeliveryPoints() =>
            Send<List<DeliveryPoint>>(RequestMethod.GET, "/v2/deliverypoints");


        /// <summary>
        /// Метод предназначен для получения списка действующих офисов СДЭК.
        /// </summary>
        public IEnumerable<DeliveryPoint> GetDeliveryPoints(DeliveryPointsRequest request) =>
            Send<List<DeliveryPoint>, DeliveryPointsRequest>(RequestMethod.GET, "/v2/deliverypoints", request);


        /// <summary>
        /// Метод предназначен для получения детальной информации о регионах.
        /// Список регионов может быть ограничен характеристиками, задаваемыми пользователем.
        /// </summary>
        public IEnumerable<Region> GetRegions() =>
            Send<List<Region>>(RequestMethod.GET, "/v2/location/regions");

        /// <summary>
        /// Метод предназначен для получения детальной информации о регионах.
        /// Список регионов может быть ограничен характеристиками, задаваемыми пользователем.
        /// </summary>
        public IEnumerable<Region> GetRegions(RegionsRequest request) =>
            Send<List<Region>, RegionsRequest>(RequestMethod.GET, "/v2/location/regions", request);


        /// <summary>
        /// Метод предназначен для получения детальной информации о населенных пунктах.
        /// Список населенных пунктов может быть ограничен характеристиками, задаваемыми пользователем.
        /// </summary>
        public IEnumerable<City> GetCities() =>
            Send<List<City>>(RequestMethod.GET, "/v2/location/cities");


        /// <summary>
        /// Метод предназначен для получения детальной информации о населенных пунктах.
        /// Список населенных пунктов может быть ограничен характеристиками, задаваемыми пользователем.
        /// </summary>
        public IEnumerable<City> GetCities(CitiesRequest request) =>
            Send<List<City>, CitiesRequest>(RequestMethod.GET, "/v2/location/cities", request);


        /// <summary>
        /// Метод предназначен для получения списка почтовых индексов.
        /// (используется вместо метода "Список населённых пунктов")
        /// </summary>
        /// <param name="code">Код города, которому принадлежат почтовые индексы.</param>
        public PostalCodeResponse GetPostalCodes(int code) =>
            Send<PostalCodeResponse>(RequestMethod.GET, $"/v2/location/postalcodes/?code={code}");


        /// <summary>
        /// Метод используется для расчета стоимости и сроков доставки по коду тарифа.
        /// </summary>
        public CalculationResponse Calculation(CalculationRequest req) {
            if (req is null)
                throw new ArgumentNullException(nameof(req));

            if (!req.TariffCode.HasValue)
                throw new Exception("Не задан код тарифа!");

            return Send<CalculationResponse, CalculationRequest>(RequestMethod.POST, "/v2/calculator/tariff", req);
        }


        /// <summary>
        /// Метод используется клиентами для расчета стоимости и сроков доставки по всем доступным тарифам.
        /// </summary>
        public MultiCalculationResponse MultiCalculation(CalculationRequest req)
        {
            if (req is null)
                throw new ArgumentNullException(nameof(req));
         
            return Send<MultiCalculationResponse, CalculationRequest>(RequestMethod.POST, "/v2/calculator/tarifflist", req);
        }


        /// <summary>
        /// Метод используется для получения перечня заказов с ссылками на готовые к скачиванию архивы.
        /// </summary>
        public OrdersResponse<OrderPhotoLink> GetPhotos(PhotoRequest req) =>
            Send<OrdersResponse<OrderPhotoLink>, PhotoRequest>(RequestMethod.POST, "/v2/photoDocument", req);
        
        /// <summary>
        /// Метод предназначен для оформления клиентских возвратов для интернет-магазинов.
        /// Клиентский возврат — это возврат, который оформляет сам клиент уже после вручения заказа.
        /// Отличие от обычного возврата в конечном статусе прямого заказа: у клиентских возвратов конечный статус "вручен" и возврат оформляет сам клиент, у обычных возвратов, конечный статус "Не вручен" и возврат оформляется СДЭКом.
        /// Для частично врученных заказов можно оформить и клиентский возврат и обычный возврат.
        /// (клиентский возврат создается только для заказов интернет-магазина в конечном статусе "Вручен")
        /// </summary>
        /// <param name="orderUuid">Идентификатор прямого заказа в ИС СДЭК</param>
        /// <param name="tariffCode">Код тарифа. Использовать тарифы, которые прописаны в договоре.</param>
        public EntityResponse<Entity> NewReturn(Guid orderUuid, int tariffCode)
        {
            var req = new ReturnRequest { TariffCode = tariffCode };
            return Send<EntityResponse<Entity>, ReturnRequest>(RequestMethod.POST, $"/v2/orders/{orderUuid}/clientReturn", req);
        }


        /// <summary>
        /// Конвертор кода валюты из международного стандарта ISO 4217 во внутренний код валюты СДЭК. 
        /// </summary>
        /// <param name="iso4217Code">Код валюты по международному стандарту ISO 4217.</param>
        /// <returns>Внутренний код валюты СДЭК</returns>
        public static int GetCdekCurrencyCode(int iso4217Code)
        {
            return iso4217Code switch
            {
                643 => 1, // Рубль
                398 => 2, // Тенге
                840 => 3, // Доллар 
                978 => 4, // Евро
                826 => 5, // Фунт стерлингов
                156 => 6, // Юань
                933 => 7, // Белорусские рубли
                980 => 8, // Гривна
                417 => 9, // Киргизский сом
                51 => 10, // Армянский драм
                949 => 11, // Турецкая лира
                764 => 12, // Тайский бат
                410 => 13, // Вона
                784 => 14, // Дирхам
                860 => 15, // Сум
                496 => 16, // Тугрик
                985 => 17, // Злотый
                944 => 18, // Манат
                981 => 19, // Лари
                392 => 55, // Японская иена
                _ => throw new NotSupportedException(),
            };
        }
    }
}
