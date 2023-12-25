using System;
using System.Collections.Generic;
using XyloCode.ThirdPartyServices.Cdek.General;

namespace XyloCode.ThirdPartyServices.Cdek.Models
{
    /// <summary>
    /// Информация о запросе доступного интервала доставки.
    /// Не применяется как модель для запроса.
    /// </summary>
    public class DeliveryIntervalRequest
    {
        /// <summary>
        /// Дата и время установки текущего состояния запроса (формат yyyy-MM-dd'T'HH:mm:ssZ).
        /// </summary>
        public DateTimeOffset DateTime { get; set; }

        /// <summary>
        /// Текущее состояние запроса.
        /// </summary>
        public string State {  get; set; }

        /// <summary>
        /// Список ошибок.
        /// </summary>
        public List<Error> Errors {  get; set; }
    }
}
