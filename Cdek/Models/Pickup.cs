using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using XyloCode.ThirdPartyServices.Cdek.General;

namespace XyloCode.ThirdPartyServices.Cdek.Models
{
    /// <summary>
    /// Регистрация заявки на вызов курьера
    /// </summary>
    public class Pickup
    {
        /// <summary>
        /// Идентификатор заявки в ИС СДЭК
        /// </summary>
        public Guid? Uuid { get; set; }


        /// <summary>
        /// Номер заявки СДЭК на вызов курьера
        /// </summary>
        public string IntakeNumber { get; set; }


        /// <summary>
        /// Номер заказа СДЭК
        /// </summary>
        public string CdekNumber { get; set; }


        /// <summary>
        /// Идентификатор заказа в ИС СДЭК
        /// </summary>
        public Guid? OrderUuid { get; set; }


        /// <summary>
        /// Дата ожидания курьера
        /// </summary>
        public DateOnly IntakeDate { get; set; }


        /// <summary>
        /// Время начала ожидания курьера
        /// </summary>
        public TimeOnly IntakeTimeFrom { get; set; }


        /// <summary>
        /// Время окончания ожидания курьера
        /// </summary>
        public TimeOnly IntakeTimeTo { get; set; }


        /// <summary>
        /// Время начала обеда, должно входить в диапозон [intake_time_from; intake_time_to]
        /// </summary>
        public TimeOnly? LunchTimeFrom { get; set; }


        /// <summary>
        /// Время окончания обеда, должно входить в диапозон [intake_time_from; intake_time_to]
        /// </summary>
        public TimeOnly? LunchTimeTo { get; set; }


        /// <summary>
        /// Описание груза
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Общий вес (в граммах)
        /// </summary>
        public int Weight { get; set; }


        /// <summary>
        /// Габариты упаковки. Длина (в сантиметрах)
        /// </summary>
        public int Length { get; set; }


        /// <summary>
        /// Габариты упаковки. Ширина (в сантиметрах)
        /// </summary>
        public int Width { get; set; }


        /// <summary>
        /// Габариты упаковки. Высота (в сантиметрах)
        /// </summary>
        public int Height { get; set; }


        /// <summary>
        /// Комментарий к заявке для курьера
        /// </summary>
        [MaxLength(255)]
        public string Comment { get; set; }


        /// <summary>
        /// Отправитель
        /// </summary>
        public Contact Sender { get; set; }


        /// <summary>
        /// Адрес отправителя (забора)
        /// </summary>
        public Location FromLocation { get; set; }


        /// <summary>
        /// Необходим прозвон отправителя (по умолчанию - false)
        /// </summary>
        public bool? NeedCall { get; set; }


        /// <summary>
        /// Статус по заявке
        /// </summary>
        public List<Status> Statuses { get; set; }

        /// <summary>
        /// Курьеру необходима доверенность (по умолчанию - false)
        /// </summary>
        public bool? CourierPowerOfAttorney { get; set; }

        /// <summary>
        /// Курьеру необходим документ удостоверяющий личность (по умолчанию - false)
        /// </summary>
        public bool? CourierIdentityCard { get; set; }
    }
}
