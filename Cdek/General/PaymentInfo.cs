using XyloCode.ThirdPartyServices.Cdek.Enums;

namespace XyloCode.ThirdPartyServices.Cdek.General
{
    /// <summary>
    /// Тип оплаты наложенного платежа получателем
    /// </summary>
    public class PaymentInfo
    {
        /// <summary>
        /// Тип оплаты:
        /// CARD - картой
        /// CASH - наличными
        /// </summary>
        public PaymentType Type { get; set; }


        /// <summary>
        /// Сумма в валюте страны получателя
        /// </summary>
        public decimal Sum { get; set; }


        /// <summary>
        /// Стоимость услуги доставки (по тарифу)
        /// </summary>
        public decimal DeliverySum { get; set; }


        /// <summary>
        /// Итоговая стоимость заказа
        /// </summary>
        public decimal TotalSum { get; set; }
    }
}
