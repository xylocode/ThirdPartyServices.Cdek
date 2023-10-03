using System;
using System.Collections.Generic;
using XyloCode.ThirdPartyServices.Cdek.Enums;

namespace XyloCode.ThirdPartyServices.Cdek.Models
{
    public class PassportStatus
    {
        /// <summary>
        /// Идентификатор заказа
        /// </summary>
        public Guid OrderUuid { get; set; }

        /// <summary>
        /// Номер заказа СДЭК
        /// </summary>
        public long? CdekNumber { get; set; }

        /// <summary>
        /// Информация о паспортных данных по заказу
        /// </summary>
        public List<CustomerPassportAccumulationStatus> Passport { get; set; }
    }

    public class CustomerPassportAccumulationStatus
    {
        /// <summary>
        /// Клиент, по которому выведена информация о наличии паспортных данных:
        /// sender - отправитель
        /// recipient - получатель
        /// </summary>
        public CustomerType Client { get; set; }

        /// <summary>
        /// Требования по паспортным данным удовлетворены (актуально для международных заказов):
        /// true - паспортные данные собраны или не требуются
        /// false - паспортные данные требуются и не собраны
        /// </summary>
        public bool PassportRequirementsSatisfied { get; set; }
    }
}
