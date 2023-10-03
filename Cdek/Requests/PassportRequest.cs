using System;
using XyloCode.ThirdPartyServices.Cdek.Enums;
using XyloCode.ThirdPartyServices.Cdek.Helpers;

namespace XyloCode.ThirdPartyServices.Cdek.Requests
{
    /// <summary>
    /// Метод используется для получения информации о паспортных данных (сообщает о готовности передавать заказы на таможню) по международным заказу/заказам.
    /// </summary>
    [IsQueryString]
    public class PassportRequest
    {
        /// <summary>
        /// Идентификатор заказа в ИС СДЭК
        /// </summary>
        public Guid?[] OrderUuid { get; set;}

        /// <summary>
        /// Номер заказа СДЭК
        /// </summary>
        public string CdekNumber { get; set; }

        /// <summary>
        /// Клиент, по которому будет выведена информация о паспортных данных:
        /// sender - отправитель
        /// recipient - получатель
        /// Если значение не передано, то информация о паспортных данных будет выводиться и по отправителю, и по получателю
        /// </summary>
        public CustomerType? Client { get; set; }
    }
}
