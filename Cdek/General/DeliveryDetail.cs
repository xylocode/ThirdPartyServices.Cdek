using System;
using System.Collections.Generic;

namespace XyloCode.ThirdPartyServices.Cdek.General
{
    /// <summary>
    /// Информация о вручении
    /// </summary>
    public class DeliveryDetail
    {
        /// <summary>
        /// Дата доставки
        /// </summary>
        public DateTime Date { get; set; }


        /// <summary>
        /// Получатель при доставке
        /// </summary>
        public string RecipientName { get; set; }


        /// <summary>
        /// Сумма наложенного платежа, которую взяли с получателя, в валюте страны получателя с учетом частичной доставки
        /// </summary>
        public decimal? PaymentSum { get; set; }


        /// <summary>
        /// Тип оплаты наложенного платежа получателем
        /// </summary>
        public List<PaymentInfo> PaymentInfo { get; set; }


        /// <summary>
        /// Стоимость услуги доставки (по тарифу).
        /// </summary>
        public decimal? DeliverySum {  get; set; }

        /// <summary>
        /// Итоговая стоимость заказа.
        /// </summary>
        public decimal? TotalSum {  get; set; }

        /// <summary>
        /// Сумма НДС для доставки.
        /// </summary>
        public decimal? DeliveryVatRate {  get; set; }

        /// <summary>
        /// Сумма НДС для доставки.
        /// </summary>
        public decimal? DeliveryVatSum {  get; set; }

        /// <summary>
        /// Процент скидки для расчёта доставки.
        /// </summary>
        public float? DeliveryDiscountPercent {  get; set; }

        /// <summary>
        /// Сумма скидки для доставки.
        /// </summary>
        public decimal? DeliveryDiscountSum {  get; set; }
    }
}
