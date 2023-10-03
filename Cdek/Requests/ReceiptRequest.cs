using System;

namespace XyloCode.ThirdPartyServices.Cdek.Requests
{
    public class ReceiptRequest
    {
        /// <summary>
        /// Идентификатор заказа в ИС СДЭК, по которому необходимо вернуть данные по чеку
        /// </summary>
        public Guid? OrderUuid { get; set; }

        /// <summary>
        /// Номер заказа СДЭК, по которому необходимо вернуть данные по чеку
        /// </summary>
        public string CdekNumber { get; set; }

        /// <summary>
        /// Дата, за которую необходимо вернуть данные по чекам
        /// </summary>
        public DateOnly? Date { get; set; }
    }
}
