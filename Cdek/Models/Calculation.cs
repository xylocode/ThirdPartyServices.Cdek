using System;
using System.Collections.Generic;
using XyloCode.ThirdPartyServices.Cdek.General;

namespace XyloCode.ThirdPartyServices.Cdek.Models
{
    /// <summary>
    /// Калькулятор
    /// </summary>
    public class CalculationRequest
    {
        /// <summary>
        /// Дата и время планируемой передачи заказа
        /// По умолчанию - текущая
        /// </summary>
        public DateOnly? Date { get; set; }

        /// <summary>
        /// Тип заказа (для проверки доступности тарифа и дополнительных услуг по типу заказа):
        /// 1 - "интернет-магазин"
        /// 2 - "доставка"
        /// По умолчанию - 1
        /// </summary>
        public int? Type { get; set; }

        /// <summary>
        /// Дополнительный тип заказа:
        /// 2 - для сборного груза(LTL)
        /// 4 - для Forward
        /// 6 - для "Фулфилмент. Приход"
        /// 7 - для "Фулфилмент. Отгрузка"
        /// </summary>
        public List<int> AdditionalOrderTypes { get; set; }

        /// <summary>
        /// Код тарифа(подробнее см. приложение 2)
        /// </summary>
        public int? TariffCode { get; set; }

        /// <summary>
        /// Валюта, в которой необходимо произвести расчет (подробнее см. приложение 1)
        /// По умолчанию - валюта договора
        /// </summary>
        public int? Currency { get; set; }

        /// <summary>
        /// Адрес отправления
        /// </summary>
        public Location FromLocation { get; set; }

        /// <summary>
        /// Адрес получения
        /// </summary>
        public Location ToLocation { get; set; }

        /// <summary>
        /// Дополнительные услуги
        /// </summary>
        public List<Service> Services { get; set; }

        /// <summary>
        /// Список информации по местам (упаковкам)
        /// </summary>
        public List<Package> Packages { get; set; }
    }
}
