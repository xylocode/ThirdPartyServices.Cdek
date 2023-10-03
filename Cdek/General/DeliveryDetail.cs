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
    }
}
