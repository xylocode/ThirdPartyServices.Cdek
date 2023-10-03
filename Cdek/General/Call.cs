using System;
using System.Collections.Generic;

namespace XyloCode.ThirdPartyServices.Cdek.General
{
    public class Call
    {
        /// <summary>
        /// Информация о неуспешных прозвонах (недозвонах)
        /// </summary>
        public List<FailedCall> FailedCalls { get; set; }

        /// <summary>
        /// Информация о переносах прозвонов
        /// </summary>
        public List<RescheduledCall> RescheduledCalls { get; set; }
    }

    /// <summary>
    /// Информация о неуспешных прозвонах (недозвонах)
    /// </summary>
    public class FailedCall
    {
        /// <summary>
        /// Дата и время создания недозвона
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Причина недозвона (подробнее см. приложение 5)
        /// </summary>
        public int ReasonCode { get; set; }
    }

    /// <summary>
    /// Информация о переносах прозвонов
    /// </summary>
    public class RescheduledCall
    {
        /// <summary>
        /// Дата и время создания переноса прозвона
        /// </summary>
        public DateTime DateTime { get; set; }


        /// <summary>
        /// Дата, на которую согласован повторный прозвон
        /// </summary>
        public DateOnly DateNext { get; set; }


        /// <summary>
        /// Время, на которое согласован повторный прозвон
        /// </summary>
        public TimeOnly TimeNext { get; set; }


        /// <summary>
        /// Комментарий к переносу прозвона
        /// </summary>
        public string Comment { get; set; }
    }
}
