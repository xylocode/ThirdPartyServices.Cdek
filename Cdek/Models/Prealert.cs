using System;
using System.Collections.Generic;

namespace XyloCode.ThirdPartyServices.Cdek.Models
{
    /// <summary>
    /// Предварительное уведомление о сдаче груза в ПВЗ
    /// </summary>
    public class Prealert
    {
        /// <summary>
        /// Идентификатор преалерта в ИС СДЭК
        /// </summary>
        public Guid? Uuid { get; set; }

        /// <summary>
        /// Номер преалерта в ИС СДЭК
        /// </summary>
        public string PrealertNumber { get; set; }

        /// <summary>
        /// Планируемая дата передачи заказов в СДЭК
        /// </summary>
        public DateTime PlannedDate { get; set; }

        /// <summary>
        /// Код ПВЗ, в который планируется передать заказы
        /// </summary>
        public string ShipmentPoint { get; set; }

        /// <summary>
        /// Дата закрытия преалерта (появляется после закрытия преалерта)
        /// </summary>
        public DateOnly? ClosedDate { get; set; }

        /// <summary>
        /// Фактический ПВЗ, в который были переданы заказы (появляется после закрытия преалерта)
        /// </summary>
        public string FactShipmentPoint { get; set; }

        /// <summary>
        /// Список заказов, которые планируется передать в СДЭК
        /// </summary>
        public List<PrealertOrder> Orders { get; set; }
    }

    /// <summary>
    /// Заказ, который планируется передать в СДЭК
    /// </summary>
    public class PrealertOrder
    {
        /// <summary>
        /// Идентификатор заказа в ИС СДЭК
        /// </summary>
        public Guid OrderUuid { get; set; }

        /// <summary>
        /// Номер заказа СДЭК
        /// </summary>
        public string CdekNumber { get; set; }

        /// <summary>
        /// Номер заказа в ИС клиента
        /// </summary>
        public string ImNumber { get; set; }

        /// <summary>
        /// Упаковки заказа, по которым получена информация о расхождениях (появляется после закрытия преалерта)
        /// </summary>
        public List<PrealertDiffPackage> Packages { get; set; }
    }

    /// <summary>
    /// Упаковки заказа, по которым получена информация о расхождениях (появляется после закрытия преалерта)
    /// </summary>
    public class PrealertDiffPackage
    {
        /// <summary>
        /// Уникальный номер упаковки в ИС СДЭК
        /// </summary>
        public Guid PackageId { get; set; }

        /// <summary>
        /// Номер упаковки в ИС клиента
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Статус упаковки:
        /// - принята
        /// - не принята
        /// - удалена
        /// </summary>
        public string Status { get; set; }
    }
}
