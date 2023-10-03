using System.Collections.Generic;

namespace XyloCode.ThirdPartyServices.Cdek.Responses
{
    public class MultiCalculationResponse : Response
    {
        /// <summary>
        /// Доступные тарифы
        /// </summary>
        public List<DeliveryOffer> TariffCodes { get; set; }
    }

    public class DeliveryOffer
    {
        /// <summary>
        /// Код тарифа (подробнее см. приложение 2)
        /// </summary>
        public int TariffCode { get; set; }

        /// <summary>
        /// Название тарифа на языке вывода
        /// </summary>
        public string TariffName { get; set; }

        /// <summary>
        /// Описание тарифа на языке вывода
        /// </summary>
        public string TariffDescription { get; set; }

        /// <summary>
        /// Режим тарифа (подробнее см. приложение 3)
        /// </summary>
        public int DeliveryMode { get; set; }

        /// <summary>
        /// Стоимость доставки
        /// </summary>
        public decimal DeliverySum { get; set; }

        /// <summary>
        /// Минимальное время доставки (в рабочих днях)
        /// </summary>
        public int PeriodMin { get; set; }

        /// <summary>
        /// Максимальное время доставки (в рабочих днях)
        /// </summary>
        public int PeriodMax { get; set; }

        /// <summary>
        /// Минимальное время доставки (в календарных днях)
        /// </summary>
        public int? CalendarMin { get; set; }

        /// <summary>
        /// Максимальное время доставки (в календарных днях)
        /// </summary>
        public int? CalendarMax { get; set; }
    }
}
