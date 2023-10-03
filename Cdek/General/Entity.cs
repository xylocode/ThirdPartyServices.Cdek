using System;
using System.ComponentModel.DataAnnotations;
using XyloCode.ThirdPartyServices.Cdek.Enums;

namespace XyloCode.ThirdPartyServices.Cdek.General
{
    /// <summary>
    /// Информация о сущности
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// Тип сущности, связанной с заказом
        /// Может принимать значения:
        /// return_order - возвратный заказ (возвращается для прямого, если заказ не вручен и по нему уже был сформирован возвратный заказ)
        /// direct_order - прямой заказ (возвращается для возвратного и реверсного заказа)
        /// waybill - квитанция к заказу (возвращается для заказа, по которому есть сформированная квитанция)
        /// barcode - ШК места к заказу (возвращается для заказа, по которому есть сформированный ШК места)
        /// reverse_order - реверсный заказ (возвращается для прямого заказа, если подключен реверс)
        /// delivery - актуальная договоренность о доставке
        /// client_return_order - заказ клиентский возврат (возвращается для прямого заказа, к которому привязан клиентский возврат)
        /// client_direct_order - прямой заказ, по которому оформлен клиентский возврат
        /// </summary>
        public EntityType? Type { get; set; }


        /// <summary>
        /// Идентификатор заказа в ИС СДЭК
        /// </summary>
        public Guid? UUID { get; set; }
    
    }

    public class LinkedEntity : Entity
    {
        /// <summary>
        /// Ссылка на скачивание печатной формы в статусе "Сформирован", только для type = waybill, barcode
        /// url может не отображаться, если ссылка на скачивание ПФ еще не сформирована.
        /// </summary>
        [MaxLength(255)]
        public string Url { get; set; }

        /// <summary>
        /// Номер заказа СДЭК
        /// Может возвращаться для return_order, direct_order, reverse_order, client_return_order, client_direct_order
        /// </summary>
        [MaxLength(255)]
        public string CdekNumber { get; set; }

        /// <summary>
        /// Дата доставки, согласованная с получателем
        /// Только для типа delivery (eсли заказ "До склада", эта дата не влияет на сроки доставки и может быть произвольной).
        /// </summary>
        public DateOnly? Date { get; set; }

        /// <summary>
        /// Время начала ожидания курьера (согласованное с получателем)
        /// Только для типа delivery
        /// </summary>
        public TimeOnly? TimeFrom { get; set; }

        /// <summary>
        /// Время окончания ожидания курьера (согласованное с получателем)
        /// Только для типа delivery
        /// </summary>
        public TimeOnly? TimeTo { get; set; }

        /// <summary>
        /// Дата и время создания сущности, связанной с заказом
        /// </summary>
        public DateTime? CreateTime { get; set; }

    }
}
