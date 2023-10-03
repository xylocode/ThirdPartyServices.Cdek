using System;
using System.Collections.Generic;
using XyloCode.ThirdPartyServices.Cdek.General;

namespace XyloCode.ThirdPartyServices.Cdek.Models
{
    public abstract class Print
    {

        /// <summary>
        /// Идентификатор квитанции к заказу
        /// </summary>
        public Guid UUID { get; set; }


        /// <summary>
        /// Список заказов
        /// </summary>
        public List<OrderId> Orders { get; set; }


        /// <summary>
        /// Число копий одной квитанции на листе
        /// </summary>
        public int? CopyCount { get; set; }

        /// <summary>
        /// Ссылка на скачивание файла.
        /// Содержится в ответе только в статусе "Сформирован"
        /// </summary>
        public string Url { get; set; }


        /// <summary>
        /// Статус квитанции
        /// </summary>
        public List<Status> Statuses { get; set; }

    }
}
