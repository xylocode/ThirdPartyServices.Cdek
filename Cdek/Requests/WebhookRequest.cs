using XyloCode.ThirdPartyServices.Cdek.Enums;

namespace XyloCode.ThirdPartyServices.Cdek.Requests
{
    public class WebhookRequest
    {
        /// <summary>
        /// URL, на который клиент хочет получать вебхуки
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Тип события
        /// </summary>
        public WebhookType Type { get; set; }
    }
}
