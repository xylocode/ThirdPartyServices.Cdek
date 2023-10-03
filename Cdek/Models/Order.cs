using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using XyloCode.ThirdPartyServices.Cdek.Enums;
using XyloCode.ThirdPartyServices.Cdek.General;

namespace XyloCode.ThirdPartyServices.Cdek.Models
{
    public class Order
    {
        /// <summary>
        /// Идентификатор заказа в ИС СДЭК
        /// </summary>
        public Guid UUID { get; set; }


        /// <summary>
        /// Признак возвратного заказа:
        /// true - возвратный
        /// false - прямой
        /// </summary>
        public bool IsReturn { get; set; }

        
        /// <summary>
        /// Признак реверсного заказа:
        /// true - реверсный
        /// false - не реверсный
        /// </summary>
        public bool IsReverse { get; set; }


        /// <summary>
        /// Признак клиентского возврата
        /// </summary>
        public bool IsClientReturn { get; set; }


        /// <summary>
        /// Тип заказа:
        /// 1 - "интернет-магазин" (только для договора типа "Договор с ИМ")
        /// 2 - "доставка" (для любого договора)
        /// </summary>
        public byte? Type { get; set; }


        /// <summary>
        /// Дополнительный тип заказа:
        /// 2 - для сборного груза(LTL)
        /// 4 - для Forward
        /// 6 - для "Фулфилмент. Приход"
        /// 7 - для "Фулфилмент. Отгрузка"
        /// </summary>
        public List<byte> AdditionalOrderTypes { get; set; }

        
        /// <summary>
        /// Номер заказа СДЭК
        /// </summary>
        [MaxLength(20)]
        public string CdekNumber { get; set; }


        /// <summary>
        /// Номер заказа в ИС Клиента (если не передан, будет присвоен номер заказа в ИС СДЭК - uuid)
        /// Только для заказов "интернет-магазин"
        /// 
        /// При запросе информации по данному полю возможны варианты:
        /// - если не передан, будет присвоен номер заказа в ИС СДЭК - uuid;
        /// - если найдено больше 1, то выбирается созданный с самой последней датой.
        /// Может содержать только цифры, буквы латинского алфавита или спецсимволы(формат ASCII)
        /// </summary>
        [MaxLength(40)]
        public string Number { get; set; }


        /// <summary>
        /// Истинный режим заказа:
        /// 1 - дверь-дверь
        /// 2 - дверь-склад
        /// 3 - склад-дверь
        /// 4 - склад-склад
        /// 6 - дверь-постамат
        /// 7 - склад-постамат
        /// 8 - постамат-дверь
        /// 9 - постамат-склад
        /// 10 - постамат-постамат
        /// </summary>
        public string DeliveryMode { get; set; }


        /// <summary>
        /// Код тарифа (подробнее см. приложение 1)
        /// </summary>
        public int TariffCode { get; set; }


        /// <summary>
        /// Комментарий к заказу
        /// Для заказов с тарифами:
        /// - "Доставка за 4 часа внутри города пешие",
        /// - "Доставка за 4 часа МСК-МО МО-МСК пешие",
        /// - "Доставка за 4 часа внутри города авто",
        /// - "Доставка за 4 часа МСК-МО МО-МСК авто",
        /// в этом поле можно передать желаемый интервал доставки заказа в формате YYYY-MM-DDThh:mm±hh;YYYY-MM-DDThh:mm±hh.
        /// Иначе по умолчанию будет выбран ближайший интервал к текущему времени.
        /// </summary>
        [MaxLength(255)]
        public string Comment { get; set; }


        /// <summary>
        /// Ключ разработчика (для разработчиков модулей)
        /// </summary>
        public string DeveloperKey { get; set; }


        /// <summary>
        /// Код ПВЗ СДЭК, на который будет производиться самостоятельный привоз клиентом
        /// Не может использоваться одновременно с from_location
        /// </summary>
        [MaxLength(255)]
        public string ShipmentPoint { get; set; }


        /// <summary>
        /// Код офиса СДЭК (ПВЗ/постамата), на который будет доставлена посылка
        /// Не может использоваться одновременно с to_location
        /// (если офис недоступен, то происходит переадресация на ближайший доступный офис)
        /// </summary>
        public string DeliveryPoint { get; set; }


        /// <summary>
        /// Дата инвойса
        /// Только для международных заказов с типом "интернет-магазин".
        /// Если поле заполнено, то заказ автоматически становится международным.
        /// </summary>
        public DateTime? DateInvoice { get; set; }

        
        /// <summary>
        /// Грузоотправитель
        /// Только для международных заказов с типом "интернет-магазин".
        /// Если поле заполнено, то заказ автоматически становится международным.
        /// </summary>
        [MaxLength(255)]
        public string ShipperName { get; set; }


        /// <summary>
        /// Адрес грузоотправителя
        /// Только для международных заказов с типом "интернет-магазин".
        /// Если поле заполнено, то заказ автоматически становится международным.
        /// </summary>
        [MinLength(255)]
        public string ShipperAddress { get; set; }


        /// <summary>
        /// Доп. сбор за доставку, которую ИМ берет с получателя.
        /// Только для заказов "интернет-магазин".
        /// </summary>
        public Money DeliveryRecipientCost { get; set; }

        
        /// <summary>
        /// Доп. сбор за доставку (которую ИМ берет с получателя) в зависимости от суммы заказа.
        /// Только для заказов "интернет-магазин".
        /// Возможно указать несколько порогов.
        /// </summary>
        public List<Threshold> DeliveryRecipientCostAdv { get; set; }

        
        /// <summary>
        /// Отправитель
        /// </summary>
        public Contact Sender { get; set; }


        /// <summary>
        /// Реквизиты истинного продавца
        /// Только для заказов "интернет-магазин"
        /// </summary>
        public Seller Seller { get; set; }


        /// <summary>
        /// Получатель
        /// </summary>
        public Contact Recipient { get; set; }

        
        /// <summary>
        /// Адрес отправления
        /// Не может использоваться одновременно с shipment_point
        /// </summary>
        public Location FromLocation { get; set; }

        
        /// <summary>
        /// Адрес получения
        /// Не может использоваться одновременно с delivery_point
        /// </summary>
        public Location ToLocation { get; set; }

        
        /// <summary>
        /// Дополнительные услуги
        /// </summary>
        public List<Service> Services { get; set; }


        /// <summary>
        /// Список информации по местам (упаковкам)
        /// Количество мест в заказе может быть от 1 до 255
        /// </summary>
        public List<Package> Packages { get; set; }


        /// <summary>
        /// Необходимость сформировать печатную форму по заказу
        /// Может принимать значения:
        /// barcode - ШК мест(число копий - 1)
        /// waybill - квитанция(число копий - 2)
        /// </summary>
        public CdekOrderPrintRequired? Print { get; set; }

        
        /// <summary>
        /// Проблемы доставки, с которыми столкнулся курьер при доставке заказа "до двери"
        /// </summary>
        public DeliveryProblem DeliveryProblem { get; set; }

        
        /// <summary>
        /// Информация о вручении
        /// </summary>
        public DeliveryDetail DeliveryDetail { get; set; }

        
        /// <summary>
        /// Признак того, что по заказу была получена информация о переводе наложенного платежа интернет-магазину
        /// </summary>
        public bool? TransactedPayment { get; set; }


        /// <summary>
        /// Список статусов по заказу, отсортированных по дате и времени
        /// </summary>
        public List<Status> Statuses { get; set; }


        /// <summary>
        /// Информация о прозвонах получателя
        /// </summary>
        public Call Calls { get; set; }


        /// <summary>
        /// Плановая дата доставки
        /// </summary>
        public DateTime? PlannedDeliveryDate { get; set; }


        /// <summary>
        /// Срок бесплатного хранения заказа на складе
        /// </summary>
        public DateTime? KeepFreeUntil { get; set; }
    }
}
