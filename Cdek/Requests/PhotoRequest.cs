using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using XyloCode.ThirdPartyServices.Cdek.General;

namespace XyloCode.ThirdPartyServices.Cdek.Requests
{
    public class PhotoRequest
    {
        /// <summary>
        /// Начало периода поиска
        /// </summary>
        [JsonPropertyName("periodBegin")]
        public DateTime? PeriodBegin { get; set; }

        /// <summary>
        /// Конец периода поиска
        /// </summary>
        [JsonPropertyName("periodEnd")]
        public DateTime? PeriodEnd { get; set; }

        /// <summary>
        /// Список заказов
        /// </summary>
        public List<OrderId> Orders { get; set; }
    }
}
