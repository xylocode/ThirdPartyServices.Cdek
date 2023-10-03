using System;

namespace XyloCode.ThirdPartyServices.Cdek.General
{
    public class OrderId
    {
        /// <summary>
        /// Идентификатор заказа в ИС СДЭК
        /// </summary>
        public Guid? OrderUuid { get; set; }

        /// <summary>
        /// Номер заказа СДЭК
        /// </summary>
        public string CdekNumber { get; set; }

        public OrderId() { }
        public OrderId(Guid uuid)
        {
            OrderUuid = uuid;
        }

        public OrderId(string number)
        {
            CdekNumber = number;
        }
    }
}
