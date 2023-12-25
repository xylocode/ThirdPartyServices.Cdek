using System;

namespace XyloCode.ThirdPartyServices.Cdek.Models
{
    /// <summary>
    /// Интервалы доступных дат для доставки
    /// </summary>
    public class DeliveryInterval
    {
        /// <summary>
        /// Дата доступного интервала для доставки (формат yyyy-MM-dd)
        /// </summary>
        public DateOnly Date {  get; set; }

        /// <summary>
        /// Временные интервалы для доставки
        /// </summary>
        public TimeInterval[] TimeIntervals {  get; set; }
    }

    /// <summary>
    /// Временные интервалы для доставки
    /// </summary>
    public class TimeInterval
    {
        /// <summary>
        /// Время начала интервала доставки
        /// </summary>
        public TimeOnly StartTime { get; set; }

        /// <summary>
        /// Время окончания интервала доставки
        /// </summary>
        public TimeOnly EndTime {  get; set; }
    }
}
