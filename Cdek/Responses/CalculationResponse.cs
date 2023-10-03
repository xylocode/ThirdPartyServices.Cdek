using System.Collections.Generic;
using XyloCode.ThirdPartyServices.Cdek.General;

namespace XyloCode.ThirdPartyServices.Cdek.Responses
{
    public class CalculationResponse : Response
    {
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
        /// Расчетный вес (в граммах)
        /// </summary>
        public int WeightCalc { get; set; }

        /// <summary>
        /// Минимальное время доставки (в календарных днях)
        /// </summary>
        public int? CalendarMin { get; set; }

        /// <summary>
        /// Максимальное время доставки (в календарных днях)
        /// </summary>
        public int? CalendarMax { get; set; }

        /// <summary>
        /// Дополнительные услуги (возвращается, если в запросе были переданы доп. услуги)
        /// </summary>
        public List<Service> Services { get; set; }

        /// <summary>
        /// Стоимость доставки с учетом дополнительных услуг
        /// </summary>
        public decimal TotalSum { get; set; }

        /// <summary>
        /// Валюта, в которой рассчитана стоимость доставки (код СДЭК)
        /// </summary>
        public string Currency { get; set; }
    }
}
