using System;
using System.Collections.Generic;
using XyloCode.ThirdPartyServices.Cdek.General;

namespace XyloCode.ThirdPartyServices.Cdek.Models
{
    /// <summary>
    /// Регистрация договоренности о доставке
    /// </summary>
    public class Delivery
    {
        /// <summary>
        /// Идентификатор договоренности о дате доставки в ИС СДЭК
        /// </summary>
        public Guid? Uuid { get; set; }

        /// <summary>
        /// Номер заказа СДЭК
        /// </summary>
        public string CdekNumber { get; set; }

        /// <summary>
        /// Идентификатор заказа в ИС СДЭК
        /// </summary>
        public Guid? OrderUuid { get; set; }

        /// <summary>
        /// Дата доставки, согласованная с получателем.
        /// Если заказ "До склада", эта дата не влияет на сроки доставки и может быть произвольной.
        /// </summary>
        public DateOnly Date { get; set; }

        /// <summary>
        /// Время начала ожидания курьера (согласованное с получателем)
        /// </summary>
        public TimeOnly TimeFrom { get; set; }

        /// <summary>
        /// Время окончания ожидания курьера (согласованное с получателем)
        /// </summary>
        public TimeOnly TimeTo { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Новый код ПВЗ СДЭК, на который будет доставлена посылка (если требовалось изменить)
        /// </summary>
        public string DeliveryPoint { get; set; }

        /// <summary>
        /// Новый адрес доставки (если требовалось изменить)
        /// </summary>
        public Location ToLocation { get; set; }

        /// <summary>
        /// Статус договоренности о доставке
        /// </summary>
        public List<Status> Statuses { get; set; }
    }
}
