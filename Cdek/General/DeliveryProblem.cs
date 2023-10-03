using System;

namespace XyloCode.ThirdPartyServices.Cdek.General
{
    /// <summary>
    /// Проблемы доставки, с которыми столкнулся курьер при доставке заказа "до двери"
    /// </summary>
    public class DeliveryProblem
    {
        /// <summary>
        /// Код проблемы (подробнее см. приложение 4)
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Дата создания проблемы
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}
