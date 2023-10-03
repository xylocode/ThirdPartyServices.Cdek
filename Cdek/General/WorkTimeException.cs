using System;

namespace XyloCode.ThirdPartyServices.Cdek.General
{
    /// <summary>
    /// Исключения в графике работы офиса
    /// </summary>
    public class WorkTimeException
    {
        /// <summary>
        /// Дата
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Период работы в указанную дату. Если в этот день не работают, то не отображается.
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// Признак рабочего/нерабочего дня в указанную дату
        /// </summary>
        public bool IsWorking { get; set; }
    }
}
