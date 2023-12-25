using System.Collections.Generic;
using XyloCode.ThirdPartyServices.Cdek.Models;

namespace XyloCode.ThirdPartyServices.Cdek.Responses
{
    /// <summary>
    /// Ответ на запрос получения доступных интервалов доставки.
    /// </summary>
    public class DeliveryIntervalsResponse
    {
        /// <summary>
        /// Интервалы доступных дат для доставки.
        /// </summary>
        public List<DeliveryInterval> DateIntervals {  get; set; }

        /// <summary>
        /// Информация о запросе/запросах.
        /// </summary>
        public List<DeliveryIntervalRequest> Requests { get; set; }
    }
}
