using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading;
using System.Threading.Tasks;
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
        const string testClientId = "wqGwiQx0gg8mLtiEKsUinjVSICCjtTEP";
        const string testClientSecret = "RmAmgvSgSl1yirlz9QupbzOJVqhCxcP5";

        private readonly string clientId;
        private readonly string clientSecret;

        private readonly HttpClient httpClient;
        private readonly JsonSerializerOptions jso;
        private readonly QueryStringSerializer qss;


        /// <summary>
        /// Признак работы с тестовой средой https://api.edu.cdek.ru
        /// </summary>
        public bool IsTest { get; } = false;


        /// <summary>
        /// Токен авторизации СДЭК, используется для возможности сохранения в хранилище приложения.
        /// </summary>
        public string AccessToken
        {
            get => httpClient.DefaultRequestHeaders?.Authorization?.Parameter;
            set => httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", value);
        }


        /// <summary>
        /// Срок жизни текущего токена авторизации СДЭК
        /// </summary>
        public long ExpiresIn { get; set; } = 0;



        public CdekClient(HttpMessageHandler httpMessageHandler = null, bool disposeHandler = true) :
            this(testClientId, testClientSecret, testBaseUri, httpMessageHandler, disposeHandler)
        {
            IsTest = true;
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
            jso.Converters.Add(new DateOnlyJsonConverter());
            jso.Converters.Add(new TimeOnlyJsonConverter());
            jso.Converters.Add(new DateTimeJsonConverter());
            jso.Converters.Add(new DateTimeOffsetJsonConverter());
            jso.Converters.Add(new JsonStringEnumConverter());


            qss = new QueryStringSerializer
            {
                NameConverter = new JsonSnakeCaseNamingPolicy().ConvertName,
                ArraySerializationMode = QueryStringSerializer.ArraySerializationModeEnum.NameOnly,
            };
        }

        static CdekClient()
        {
            var currencies = DeserializeList<Currency>(Properties.Resources.CurrenciesJson);
            Currencies = new(currencies.ToDictionary(x => x.CdekCurrencyId));
            Currencies2 = new(currencies.ToDictionary(x => x.Id, x => x.CdekCurrencyId));

            var tariffs = DeserializeList<Tariff>(Properties.Resources.TariffsJson)
                .ToDictionary(x => x.Id);
            Tariffs = new(tariffs);

            var deliveryModes = DeserializeList<IdName>(Properties.Resources.DeliveryModesJson)
                .ToDictionary(x => x.Id, x => x.Name);
            DeliveryModes = new(deliveryModes);

            var additionalOrderStatuses = DeserializeList<IdName>(Properties.Resources.AdditionalOrderStatusesJson)
                .ToDictionary(x => x.Id, x => x.Name);
            AdditionalOrderStatuses = new(additionalOrderStatuses);

            var deliveryProblems = DeserializeList<IdName>(Properties.Resources.DeliveryProblemsJson)
                .ToDictionary(x => x.Id, x => x.Name);
            DeliveryProblems = new(deliveryProblems);

            var nonCallReasons = DeserializeList<IdName>(Properties.Resources.NonCallReasonsJson)
                .ToDictionary(x => x.Id, x => x.Name);
            NonCallReasons = new(nonCallReasons);
        }

        private enum RequestMethod : byte
        {
            GET,
            POST,
            PUT,
            DELETE,
            PATCH,
        }

        private async Task<TRes> SendAsync<TRes>(RequestMethod method, string path, CancellationToken cancellationToken = default)
        {
            if (DateTime.UtcNow.Ticks > ExpiresIn)
                await AuthAsync(cancellationToken);
            Thread.Sleep(1000);
            HttpResponseMessage res = method switch
            {
                RequestMethod.GET => await httpClient.GetAsync(path, HttpCompletionOption.ResponseHeadersRead, cancellationToken),
                RequestMethod.DELETE => await httpClient.DeleteAsync(path, cancellationToken),
                _ => throw new NotSupportedException(),
            };
            return await res.Content.ReadFromJsonAsync<TRes>(jso, cancellationToken);
        }


        private async Task<TRes> SendAsync<TRes, TReq>(RequestMethod method, string path, TReq req, CancellationToken cancellationToken = default)
        {
            if (DateTime.UtcNow.Ticks > ExpiresIn)
                await AuthAsync(cancellationToken);

            Thread.Sleep(1000);

            if (method == RequestMethod.GET || method == RequestMethod.DELETE)
                path = string.Concat(path, "?", qss.Serialize(req));


            HttpResponseMessage res = method switch
            {
                RequestMethod.GET => await httpClient.GetAsync(path, HttpCompletionOption.ResponseHeadersRead, cancellationToken),
                RequestMethod.POST => await httpClient.PostAsJsonAsync(path, req, jso, cancellationToken),
                RequestMethod.PUT => await httpClient.PutAsJsonAsync(path, req, jso, cancellationToken),
                RequestMethod.DELETE => await httpClient.DeleteAsync(path, cancellationToken),
                RequestMethod.PATCH => await httpClient.PatchAsJsonAsync(path, req, jso, cancellationToken),
                _ => throw new NotSupportedException(),
            };

            return await res.Content.ReadFromJsonAsync<TRes>(jso, cancellationToken);
        }


        private async IAsyncEnumerable<TRes> GetAsyncEnumerable<TRes>(string path, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            if (DateTime.UtcNow.Ticks > ExpiresIn)
                await AuthAsync(cancellationToken);
            Thread.Sleep(1000);

            var res = await httpClient.GetAsync(path, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            await foreach (var item in res.Content.ReadFromJsonAsAsyncEnumerable<TRes>(jso, cancellationToken))
                yield return item;
        }

        private async IAsyncEnumerable<TRes> GetAsyncEnumerable<TRes, TReq>(string path, TReq req, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            if (DateTime.UtcNow.Ticks > ExpiresIn)
                await AuthAsync(cancellationToken);
            Thread.Sleep(1000);

            path = string.Concat(path, "?", qss.Serialize(req));
            var res = await httpClient.GetAsync(path, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            await foreach (var item in res.Content.ReadFromJsonAsAsyncEnumerable<TRes>(jso, cancellationToken))
                yield return item;
        }


        /// <summary>
        /// Запрос JWT-токена у сервера авторизации.
        /// Метод используется когда токен нужно хранить во внутреннем хранилище приложения, в иных случаях метод вызывается автоматически.
        /// </summary>
        public async Task AuthAsync(CancellationToken cancellationToken = default)
        {
            var oauth = new Dictionary<string, string>
            {
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "grant_type", "client_credentials" }
            };
            var form = new FormUrlEncodedContent(oauth);
            httpClient.DefaultRequestHeaders.Authorization = null;
            var res = await httpClient.PostAsync("/v2/oauth/token?parameters", form, cancellationToken);
            var auth = await res.Content.ReadFromJsonAsync<Models.Authorization>(jso, cancellationToken);

            this.AccessToken = auth.AccessToken;
            this.ExpiresIn = DateTime.UtcNow.AddSeconds(auth.ExpiresIn).Ticks;
        }


        /// <summary>
        /// Метод предназначен для создания в ИС СДЭК заказа на доставку товаров до покупателей.
        /// Выделяется 2 типа заказов:
        /// “интернет-магазин” - может быть только у клиента с типом договора “Интернет-магазин”;
        /// “доставка” может быть создан любым клиентом с договором(но доступны тарифы только для обычной доставки).
        /// </summary>
        public Task<EntityResponse<Entity>> NewOrderAsync(Order order, CancellationToken cancellationToken = default) =>
            SendAsync<EntityResponse<Entity>, Order>(RequestMethod.POST, "/v2/orders", order, cancellationToken);


        /// <summary>
        /// Метод предназначен для получения детальной информации по заданному заказу.
        /// Есть возможность получить информацию в том числе о заказах, которые были созданы через другие каналы(личный кабинет, протокол v.1.5 и др.).
        /// Но только по тем, которые были созданы после выдачи индивидуальных ключей доступа.
        /// <summary>
        /// <param name="uuid">Идентификатор заказа в ИС СДЭК, по которому необходима информация</param>
        /// <param name="cancellationToken">Токен отмены, который может использоваться другими объектами или потоками для получения уведомления об отмене.</param>
        public Task<EntityResponse<Order>> GetOrderAsync(Guid uuid, CancellationToken cancellationToken = default) =>
            SendAsync<EntityResponse<Order>>(RequestMethod.GET, $"/v2/orders/{uuid}", cancellationToken);


        /// <summary>
        /// Метод предназначен для получения детальной информации по заданному заказу.
        /// Есть возможность получить информацию в том числе о заказах, которые были созданы через другие каналы(личный кабинет, протокол v.1.5 и др.).
        /// Но только по тем, которые были созданы после выдачи индивидуальных ключей доступа.
        /// <summary>
        /// <param name="cdekNumber">Номер заказа СДЭК, по которому необходима информация</param>
        /// <param name="cancellationToken">Токен отмены, который может использоваться другими объектами или потоками для получения уведомления об отмене.</param>
        public Task<EntityResponse<Order>> GetOrderAsync(string cdekNumber, CancellationToken cancellationToken = default) =>
            SendAsync<EntityResponse<Order>>(RequestMethod.GET, $"/v2/orders?cdek_number={cdekNumber}", cancellationToken);


        /// <summary>
        /// Метод предназначен для получения детальной информации по заданному заказу.
        /// Есть возможность получить информацию в том числе о заказах, которые были созданы через другие каналы(личный кабинет, протокол v.1.5 и др.).
        /// Но только по тем, которые были созданы после выдачи индивидуальных ключей доступа.
        /// </summary>
        /// <param name="number">Номер заказа в ИС Клиента, по которому необходима информация</param>
        /// <param name="cancellationToken">Токен отмены, который может использоваться другими объектами или потоками для получения уведомления об отмене.</param>
        public Task<EntityResponse<Order>> GetOrderByInternalNumberAsync(string number, CancellationToken cancellationToken = default) =>
            SendAsync<EntityResponse<Order>>(RequestMethod.GET, $"/v2/orders?im_number={number}", cancellationToken);


        /// <summary>
        /// Метод используется для изменения созданного ранее заказа.
        /// Условием возможности изменения заказа является отсутствие движения груза на складе СДЭК (т.е.статус заказа «Создан»).
        /// </summary>
        public Task<EntityResponse<Entity>> EditOrderAsync(Order order, CancellationToken cancellationToken = default) =>
            SendAsync<EntityResponse<Entity>, Order>(RequestMethod.PATCH, "/v2/orders", order, cancellationToken);


        /// <summary>
        /// Метод предназначен для удаления заказа.
        /// Условием возможности удаления заказа является отсутствие движения груза на складе СДЭК (статус заказа «Создан»).
        /// </summary>
        /// <param name="uuid">Идентификатор заказа в ИС СДЭК, который необходимо удалить</param>
        /// <param name="cancellationToken">Токен отмены, который может использоваться другими объектами или потоками для получения уведомления об отмене.</param>
        public Task<EntityResponse<Entity>> DeleteOrderAsync(Guid uuid, CancellationToken cancellationToken = default) =>
            SendAsync<EntityResponse<Entity>>(RequestMethod.DELETE, $"/v2/orders/{uuid}", cancellationToken);


        /// <summary>
        /// Метод предназначен для регистрации отказа по заказу и дальнейшего возврата данного заказа в интернет-магазин.
        /// После успешной регистрации отказа статус заказа переходит в "Не вручен" (код NOT_DELIVERED) с дополнительным статусом "Возврат, отказ от получения: Без объяснения" (код 11).
        /// Заказ может быть отменен в любом статусе, пока не установлен статус "Вручен" или "Не вручен".
        /// </summary>
        /// <param name="uuid">Идентификатор заказа в ИС СДЭК, по которому необходимо зарегистрировать отказ</param>
        /// <param name="cancellationToken">Токен отмены, который может использоваться другими объектами или потоками для получения уведомления об отмене.</param>
        public Task<EntityResponse<Entity>> RefusalAsync(Guid uuid, CancellationToken cancellationToken = default) =>
            SendAsync<EntityResponse<Entity>>(RequestMethod.POST, $"/v2/orders/{uuid}/refusal", cancellationToken);


        /// <summary>
        /// Метод позволяет осуществить вызов курьера для забора груза со склада ИМ с последующей доставкой до склада СДЭК.
        /// Рекомендуемый минимальный диапазон времени для приезда курьера не менее 3х часов.
        /// </summary>
        public Task<EntityResponse<Entity>> NewPickupAsync(Pickup pickup, CancellationToken cancellationToken = default) =>
            SendAsync<EntityResponse<Entity>, Pickup>(RequestMethod.POST, "/v2/intakes", pickup, cancellationToken);


        /// <summary>
        /// Метод предназначен для получения информации по заявке на вызов курьера.
        /// </summary>
        /// <param name="uuid">Идентификатор заявки в ИС СДЭК, по которому необходима информация</param>
        /// <param name="cancellationToken">Токен отмены, который может использоваться другими объектами или потоками для получения уведомления об отмене.</param>
        public Task<EntityResponse<Pickup>> GetPickupAsync(Guid uuid, CancellationToken cancellationToken = default) =>
            SendAsync<EntityResponse<Pickup>>(RequestMethod.GET, $"/v2/intakes/{uuid}", cancellationToken);


        /// <summary>
        /// Метод предназначен для удаление заявки на вызов курьера.
        /// </summary>
        /// <param name="uuid">Идентификатор заявки в ИС СДЭК, которую необходимо удалить</param>
        /// <param name="cancellationToken">Токен отмены, который может использоваться другими объектами или потоками для получения уведомления об отмене.</param>
        public Task<EntityResponse<Entity>> DeletePickupAsync(Guid uuid, CancellationToken cancellationToken = default) =>
            SendAsync<EntityResponse<Entity>>(RequestMethod.DELETE, $"/v2/intakes/{uuid}", cancellationToken);


        /// <summary>
        /// Метод используется для формирования квитанции в формате pdf к заказу/заказам.
        /// </summary>
        public Task<EntityResponse<Entity>> WaybillRequestAsync(WaybillRequest req, CancellationToken cancellationToken = default) =>
            SendAsync<EntityResponse<Entity>, WaybillRequest>(RequestMethod.POST, "/v2/print/orders", req, cancellationToken);


        /// <summary>
        /// Метод используется для получения ссылки на квитанцию в формате pdf к заказу/заказам.
        /// </summary>
        /// <param name="uuid">Идентификатор квитанции, ссылку на которую необходимо получить</param>
        /// <param name="cancellationToken">Токен отмены, который может использоваться другими объектами или потоками для получения уведомления об отмене.</param>
        public Task<EntityResponse<Waybill>> GetWaybillAsync(Guid uuid, CancellationToken cancellationToken = default) =>
            SendAsync<EntityResponse<Waybill>>(RequestMethod.GET, $"/v2/print/orders/{uuid}", cancellationToken);


        /// <summary>
        /// Метод используется для формирования ШК места в формате pdf к заказу/заказам.
        /// Во избежание перегрузки платформы нельзя передавать более 100 номеров заказов в одном запросе.
        /// </summary>
        public Task<EntityResponse<Entity>> BarcodeRequestAsync(BarcodeRequest req, CancellationToken cancellationToken = default) =>
            SendAsync<EntityResponse<Entity>, BarcodeRequest>(RequestMethod.POST, "/v2/print/barcodes", req, cancellationToken);


        /// <summary>
        /// Метод используется для получения ШК места в формате pdf к заказу/заказам.
        /// </summary>
        /// <param name="uuid">Идентификатор ШК места, ссылку на который необходимо получить</param>
        /// <param name="cancellationToken">Токен отмены, который может использоваться другими объектами или потоками для получения уведомления об отмене.</param>
        public Task<EntityResponse<Barcode>> GetBarcodeAsync(Guid uuid, CancellationToken cancellationToken = default) =>
            SendAsync<EntityResponse<Barcode>>(RequestMethod.GET, $"/v2/print/barcodes/{uuid}", cancellationToken
                );


        /// <summary>
        /// Метод позволяет фиксировать оговоренные с клиентом дату и время доставки (приезда курьера), а так же изменять адрес доставки.
        /// </summary>
        public Task<EntityResponse<Entity>> NewDeliveryAsync(Delivery delivery, CancellationToken cancellationToken = default) =>
            SendAsync<EntityResponse<Entity>, Delivery>(RequestMethod.POST, "/v2/delivery", delivery, cancellationToken);


        /// <summary>
        /// Метод используется для получения информации об оговоренных с клиентом дате и времени доставки (приезда курьера), а так же возможном новом адресе доставки.
        /// </summary>
        /// <param name="uuid">Идентификатор договоренности о доставке в ИС СДЭК.</param>
        /// <param name="cancellationToken">Токен отмены, который может использоваться другими объектами или потоками для получения уведомления об отмене.</param>
        public Task<EntityResponse<Delivery>> GetDeliveryAsync(Guid uuid, CancellationToken cancellationToken = default) =>
            SendAsync<EntityResponse<Delivery>>(RequestMethod.GET, $"/v2/delivery/{uuid}", cancellationToken);


        /// <summary>
        /// Метод предназначен для регистрации преалерта (реестра заказов, которые клиент собирается передать на склад СДЭК для дальнейшей доставки).
        /// </summary>
        public Task<EntityResponse<Entity>> NewPrealertAsync(Prealert prealert, CancellationToken cancellationToken = default) =>
            SendAsync<EntityResponse<Entity>, Prealert>(RequestMethod.POST, "/v2/prealert", prealert, cancellationToken);


        /// <summary>
        /// Метод предназначен для получения информации по заданному преалерту.
        /// </summary>
        /// <param name="uuid">Идентификатор преалерта в ИС СДЭК, по которому необходима информация</param>
        /// <param name="cancellationToken">Токен отмены, который может использоваться другими объектами или потоками для получения уведомления об отмене.</param>
        public Task<EntityResponse<Prealert>> GetPrealertAsync(Guid uuid, CancellationToken cancellationToken = default) =>
            SendAsync<EntityResponse<Prealert>>(RequestMethod.GET, $"/v2/prealert/{uuid}", cancellationToken);


        /// <summary>
        /// Метод используется для получения информации о паспортных данных (сообщает о готовности передавать заказы на таможню) по международным заказу/заказам.
        /// </summary>
        public Task<OrdersResponse<PassportStatus>> GetPassportInfoAsync(PassportRequest req, CancellationToken cancellationToken = default) =>
            SendAsync<OrdersResponse<PassportStatus>, PassportRequest>(RequestMethod.GET, $"/v2/passport", req, cancellationToken);


        /// <summary>
        /// Метод используется для получения информации о чеке по заказу или за выбранный день.
        /// </summary>
        /// <param name="orderUuid">Идентификатор заказа в ИС СДЭК, по которому необходимо вернуть данные по чеку</param>
        /// <param name="cdekNumber">Номер заказа СДЭК, по которому необходимо вернуть данные по чеку</param>
        /// <param name="date">Дата, за которую необходимо вернуть данные по чекам</param>
        /// <param name="cancellationToken">Токен отмены, который может использоваться другими объектами или потоками для получения уведомления об отмене.</param>
        public Task<ReceiptResponse> GetReceiptAsync(Guid? orderUuid = null, string cdekNumber = null, DateOnly? date = null, CancellationToken cancellationToken = default)
        {
            var req = new ReceiptRequest
            {
                OrderUuid = orderUuid,
                CdekNumber = cdekNumber,
                Date = date
            };
            return SendAsync<ReceiptResponse, ReceiptRequest>(RequestMethod.GET, $"/v2/check", req, cancellationToken
                );
        }


        /// <summary>
        /// Метод предназначен для получения информации о реестрах наложенных платежей, по которым клиенту был переведен наложенный платеж в заданную клиентом дату.
        /// </summary>
        /// <param name="date">Дата, за которую необходимо вернуть реестры наложенных платежей, по которым был переведен наложенный платеж</param>
        /// <param name="cancellationToken">Токен отмены, который может использоваться другими объектами или потоками для получения уведомления об отмене.</param>
        public Task<CashOnDeliveryRegisterResponse> GetCashOnDeliveryRegisterAsync(DateOnly date, CancellationToken cancellationToken = default)
        {
            var dateStr = date.ToString("yyyy-MM-dd");
            return SendAsync<CashOnDeliveryRegisterResponse>(RequestMethod.GET, $"/v2/registries?date={dateStr}", cancellationToken);
        }


        /// <summary>
        /// Метод предназначен для получения информации о заказах, по которым был переведен наложенный платеж интернет-магазину в заданную дату.
        /// </summary>
        /// <param name="date">Дата, за которую необходимо вернуть список заказов, по которым был переведен наложенный платеж</param>
        /// <param name="cancellationToken">Токен отмены, который может использоваться другими объектами или потоками для получения уведомления об отмене.</param>
        public Task<OrdersResponse<OrderId2>> GetWireTransferAsync(DateOnly date, CancellationToken cancellationToken = default)
        {
            var dateStr = date.ToString("yyyy-MM-dd");
            return SendAsync<OrdersResponse<OrderId2>>(RequestMethod.GET, $"/v2/payment?date={dateStr}", cancellationToken);
        }


        /// <summary>
        /// Предназначены для отправки на URL клиента событий:
        /// изменении статуса заказа;
        /// готовности печатной формы.
        /// </summary>
        /// <param name="type">Тип события</param>
        /// <param name="url">URL, на который клиент хочет получать вебхуки</param>
        /// <param name="cancellationToken">Токен отмены, который может использоваться другими объектами или потоками для получения уведомления об отмене.</param>
        public Task<EntityResponse<Entity>> NewWebhookAsync(WebhookType type, string url, CancellationToken cancellationToken = default)
        {
            if (IsTest)
                throw new NotImplementedException();

            var req = new WebhookRequest { Type = type, Url = url };
            return SendAsync<EntityResponse<Entity>, WebhookRequest>(RequestMethod.POST, "/v2/webhooks", req, cancellationToken);
        }


        /// <summary>
        /// Метод предназначен для получения списка действующих офисов СДЭК.
        /// </summary>
        public IAsyncEnumerable<DeliveryPoint> GetDeliveryPointsAsync([EnumeratorCancellation] CancellationToken cancellationToken = default) =>
            GetAsyncEnumerable<DeliveryPoint>("/v2/deliverypoints", cancellationToken);


        /// <summary>
        /// Метод предназначен для получения списка действующих офисов СДЭК.
        /// </summary>
        public IAsyncEnumerable<DeliveryPoint> GetDeliveryPointsAsync(DeliveryPointsRequest request, [EnumeratorCancellation] CancellationToken cancellationToken = default) =>
            GetAsyncEnumerable<DeliveryPoint, DeliveryPointsRequest>("/v2/deliverypoints", request, cancellationToken);


        /// <summary>
        /// Метод предназначен для получения детальной информации о регионах.
        /// Список регионов может быть ограничен характеристиками, задаваемыми пользователем.
        /// </summary>
        public IAsyncEnumerable<Region> GetRegionsAsync([EnumeratorCancellation] CancellationToken cancellationToken = default) =>
            GetAsyncEnumerable<Region>("/v2/location/regions", cancellationToken);


        /// <summary>
        /// Метод предназначен для получения детальной информации о регионах.
        /// Список регионов может быть ограничен характеристиками, задаваемыми пользователем.
        /// </summary>
        public IAsyncEnumerable<Region> GetRegionsAsync(RegionsRequest request, [EnumeratorCancellation] CancellationToken cancellationToken = default) =>
            GetAsyncEnumerable<Region, RegionsRequest>("/v2/location/regions", request, cancellationToken);


        /// <summary>
        /// Метод предназначен для получения детальной информации о населенных пунктах.
        /// Список населенных пунктов может быть ограничен характеристиками, задаваемыми пользователем.
        /// </summary>
        public IAsyncEnumerable<City> GetCitiesAsync([EnumeratorCancellation] CancellationToken cancellationToken = default) =>
            GetAsyncEnumerable<City>("/v2/location/cities", cancellationToken);


        /// <summary>
        /// Метод предназначен для получения детальной информации о населенных пунктах.
        /// Список населенных пунктов может быть ограничен характеристиками, задаваемыми пользователем.
        /// </summary>
        public IAsyncEnumerable<City> GetCitiesAsync(CitiesRequest request, [EnumeratorCancellation] CancellationToken cancellationToken = default) =>
            GetAsyncEnumerable<City, CitiesRequest>("/v2/location/cities", request, cancellationToken);


        /// <summary>
        /// Метод предназначен для получения списка почтовых индексов.
        /// (используется вместо метода "Список населённых пунктов")
        /// </summary>
        /// <param name="code">Код города, которому принадлежат почтовые индексы.</param>
        /// <param name="cancellationToken">Токен отмены, который может использоваться другими объектами или потоками для получения уведомления об отмене.</param>
        public Task<PostalCodeResponse> GetPostalCodesAsync(int code, CancellationToken cancellationToken = default) =>
            SendAsync<PostalCodeResponse>(RequestMethod.GET, $"/v2/location/postalcodes/?code={code}", cancellationToken);


        /// <summary>
        /// Метод используется для расчета стоимости и сроков доставки по коду тарифа.
        /// </summary>
        public Task<CalculationResponse> CalculationAsync(CalculationRequest req, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(req);

            if (!req.TariffCode.HasValue)
                throw new Exception("Не задан код тарифа!");

            return SendAsync<CalculationResponse, CalculationRequest>(RequestMethod.POST, "/v2/calculator/tariff", req, cancellationToken);
        }


        /// <summary>
        /// Метод используется клиентами для расчета стоимости и сроков доставки по всем доступным тарифам.
        /// </summary>
        public Task<MultiCalculationResponse> MultiCalculationAsync(CalculationRequest req, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(req);

            return SendAsync<MultiCalculationResponse, CalculationRequest>(RequestMethod.POST, "/v2/calculator/tarifflist", req, cancellationToken);
        }


        /// <summary>
        /// Метод используется для получения перечня заказов с ссылками на готовые к скачиванию архивы.
        /// </summary>
        public Task<OrdersResponse<OrderPhotoLink>> GetPhotosAsync(PhotoRequest req, CancellationToken cancellationToken = default) =>
            SendAsync<OrdersResponse<OrderPhotoLink>, PhotoRequest>(RequestMethod.POST, "/v2/photoDocument", req, cancellationToken);


        /// <summary>
        /// Метод предназначен для оформления клиентских возвратов для интернет-магазинов.
        /// Клиентский возврат — это возврат, который оформляет сам клиент уже после вручения заказа.
        /// Отличие от обычного возврата в конечном статусе прямого заказа: у клиентских возвратов конечный статус "вручен" и возврат оформляет сам клиент, у обычных возвратов, конечный статус "Не вручен" и возврат оформляется СДЭКом.
        /// Для частично врученных заказов можно оформить и клиентский возврат и обычный возврат.
        /// (клиентский возврат создается только для заказов интернет-магазина в конечном статусе "Вручен")
        /// </summary>
        /// <param name="orderUuid">Идентификатор прямого заказа в ИС СДЭК</param>
        /// <param name="tariffCode">Код тарифа. Использовать тарифы, которые прописаны в договоре.</param>
        /// <param name="cancellationToken">Токен отмены, который может использоваться другими объектами или потоками для получения уведомления об отмене.</param>
        public Task<EntityResponse<Entity>> NewReturnAsync(Guid orderUuid, int tariffCode, CancellationToken cancellationToken = default)
        {
            var req = new ReturnRequest { TariffCode = tariffCode };
            return SendAsync<EntityResponse<Entity>, ReturnRequest>(RequestMethod.POST, $"/v2/orders/{orderUuid}/clientReturn", req, cancellationToken);
        }


        /// <summary>
        /// Метод используется для получения доступных интервалов доставки.
        /// </summary>
        /// <param name="orderUuid">Идентификатор заказа в ИС СДЭК</param>
        /// <param name="cancellationToken">Токен отмены, который может использоваться другими объектами или потоками для получения уведомления об отмене.</param>
        public Task<DeliveryIntervalsResponse> GetDeliveryIntervalsAsync(Guid orderUuid, CancellationToken cancellationToken = default) =>
            SendAsync<DeliveryIntervalsResponse>(RequestMethod.GET, $"/v2/delivery/intervals?order_uuid={orderUuid}", cancellationToken);

        /// <summary>
        /// Метод используется для получения доступных интервалов доставки.
        /// </summary>
        /// <param name="cdekNumber">Номер заказа СДЭК</param>
        /// <param name="cancellationToken">Токен отмены, который может использоваться другими объектами или потоками для получения уведомления об отмене.</param>
        public Task<DeliveryIntervalsResponse> GetDeliveryIntervalsAsync(string cdekNumber, CancellationToken cancellationToken = default) =>
            SendAsync<DeliveryIntervalsResponse>(RequestMethod.GET, $"/v2/delivery/intervals?cdek_number={cdekNumber}", cancellationToken);

        #region Статические справочники
        /// <summary>
        /// Приложение 1. Валюты калькулятора
        /// </summary>
        public static ReadOnlyDictionary<int, Currency> Currencies { get; private set; }
        public static ReadOnlyDictionary<int, int> Currencies2 { get; private set; }

        /// <summary>
        /// Приложение 2. Тарифы СДЭК
        /// </summary>
        public static ReadOnlyDictionary<int, Tariff> Tariffs { get; private set; }

        /// <summary>
        /// Приложение 3. Режимы доставки
        /// </summary>
        public static ReadOnlyDictionary<int, string> DeliveryModes { get; private set; }

        /// <summary>
        /// Приложение 2. Дополнительные статусы заказов
        /// </summary>
        public static ReadOnlyDictionary<int, string> AdditionalOrderStatuses { get; private set; }

        /// <summary>
        /// Приложение 4. Проблемы доставки
        /// </summary>
        public static ReadOnlyDictionary<int, string> DeliveryProblems { get; private set; }

        /// <summary>
        /// Приложение 5. Причины недозвона
        /// </summary>
        public static ReadOnlyDictionary<int, string> NonCallReasons { get; private set; }


        /// <summary>
        /// Конвертор кода валюты из международного стандарта ISO 4217 во внутренний код валюты СДЭК. 
        /// </summary>
        /// <param name="iso4217Code">Код валюты по международному стандарту ISO 4217.</param>
        /// <returns>Внутренний код валюты СДЭК</returns>
        public static int GetCdekCurrencyCode(int iso4217Code)
        {
            return Currencies2[iso4217Code];
        }

        private static IEnumerable<T> DeserializeList<T>(byte[] fileData)
            where T : class, new()
        {
            var json = GetFileData(fileData);
            return JsonSerializer.Deserialize<List<T>>(json);
        }

        private static string GetFileData(byte[] bytes)
        {
            var preamble = Encoding.UTF8.GetPreamble();
            bool withPreample = true;
            if (bytes.Length > preamble.Length)
            {
                for (int i = 0; i < preamble.Length; i++)
                {
                    withPreample &= bytes[i] == preamble[i];
                }
            }

            if (withPreample)
                return Encoding.UTF8.GetString(bytes, preamble.Length, bytes.Length - preamble.Length);
            else
                return Encoding.UTF8.GetString(bytes);
        }
        #endregion Статические справочники
    }
}
