using System.Collections.Generic;
using XyloCode.ThirdPartyServices.Cdek.Enums;
using XyloCode.ThirdPartyServices.Cdek.General;

namespace XyloCode.ThirdPartyServices.Cdek.Requests
{
    public class WaybillRequest
    {
        /// <summary>
        /// Список заказов
        /// </summary>
        public List<OrderId> Orders { get; set; }

        /// <summary>
        /// Число копий одной квитанции на листе.
        /// Рекомендовано указывать не менее 2, одна приклеивается на груз, вторая остается у отправителя
        /// По умолчанию 2
        /// </summary>
        public int? CopyCount { get; set; }

        /// <summary>
        /// Форма квитанции
        /// </summary>
        public WaybillType? Type { get; set; }
    }
}
