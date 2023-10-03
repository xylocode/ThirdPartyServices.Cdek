using System;
using System.Collections.Generic;
using XyloCode.ThirdPartyServices.Cdek.Enums;
using XyloCode.ThirdPartyServices.Cdek.General;

namespace XyloCode.ThirdPartyServices.Cdek.Models
{
    public class Receipt
    {
        /// <summary>
        /// Идентификатор заказа в ИС СДЭК, по которому был сформирован чек
        /// </summary>
        public Guid OrderUuid { get; set; }

        /// <summary>
        /// Номер заказа СДЭК, по которому был сформирован чек
        /// </summary>
        public string CdekNumber { get; set; }

        /// <summary>
        /// Дата и время формирования чека
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Номер фискального накопителя
        /// </summary>
        public string FiscalStorageNumber { get; set; }

        /// <summary>
        /// Порядковый номер фискального документа
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Фискальный признак документа
        /// </summary>
        public string FiscalSign { get; set; }

        /// <summary>
        /// Тип чека. Возможные значения:
        /// CASH_RECEIPT_IN - чек прихода
        /// CASH_RECEIPT_REFUND - чек возврата
        /// </summary>
        public ReceiptType Type { get; set; }

        /// <summary>
        /// Информация о сумме по чеку
        /// </summary>
        public List<PaymentInfo> PaymentInfo { get; set; }
    }
}
