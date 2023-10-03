using System;
using System.ComponentModel.DataAnnotations;
using XyloCode.ThirdPartyServices.Cdek.Enums;

namespace XyloCode.ThirdPartyServices.Cdek.General
{
    /// <summary>
    /// Статус заказа, заявки
    /// </summary>
    public class Status
    {
        /// <summary>
        /// Код статуса
        /// </summary>
        public StatusCode Code { get; set; }

        /// <summary>
        /// Название статуса
        /// </summary>
        [MaxLength(255)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Дата и время установки статуса (формат yyyy-MM-dd'T'HH:mm:ssZ)
        /// </summary>
        public DateTimeOffset? DateTime { get; set; }

        /// <summary>
        /// Дополнительный код статуса
        /// </summary>
        [MaxLength(2)]
        public string ReasonCode { get; set; }

        /// <summary>
        /// Наименование места возникновения статуса
        /// </summary>
        [MaxLength(255)]
        public string City { get; set; }
    }
}
