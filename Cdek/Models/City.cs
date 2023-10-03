using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using XyloCode.ThirdPartyServices.Cdek.General;

namespace XyloCode.ThirdPartyServices.Cdek.Models
{
    /// <summary>
    /// Населенный пункт СДЭК
    /// </summary>
    public class City
    {
        /// <summary>
        /// Код населенного пункта СДЭК
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Название населенного пункта.
        /// </summary>
        [MaxLength(255)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Уникальный идентификатор ФИАС населенного пункта
        /// </summary>
        public Guid? FiasGuid { get; set; }

        /// <summary>
        /// Идентификатор города в ИС СДЭК
        /// </summary>
        public Guid CityUuid { get; set; }

        /// <summary>
        /// Код КЛАДР населенного пункта
        /// </summary>
        [Obsolete]
        [MaxLength(255)]
        public string KladrCode { get; set; }

        /// <summary>
        /// Код страны населенного пункта в формате  ISO_3166-1_alpha-2
        /// </summary>
        [MaxLength(2)]
        [Required]
        public string CountryCode { get; set; }

        /// <summary>
        /// Название страны населенного пункта
        /// </summary>
        [MaxLength (255)]
        [Required]
        public string Country { get; set; }

        /// <summary>
        /// Название региона населенного пункта
        /// </summary>
        [MaxLength(255)]
        public string Region { get; set; }

        /// <summary>
        /// Код региона СДЭК
        /// </summary>
        public int? RegionCode { get; set; }

        /// <summary>
        /// Уникальный идентификатор ФИАС региона населенного пункта
        /// </summary>
        [Obsolete]
        public Guid? FiasRegionGuid { get; set; }

        /// <summary>
        /// Код КЛАДР региона населенного пункта
        /// </summary>
        [Obsolete]
        [MaxLength(255)]
        public string KladrRegionCode { get; set; }

        /// <summary>
        /// Название района региона населенного пункта
        /// </summary>
        [MaxLength(255)]
        public string SubRegion { get; set; }

        /// <summary>
        /// Долгота центра населенного пункта
        /// </summary>
        public double? Longitude { get; set; }

        /// <summary>
        /// Широта центра населенного пункта
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        /// Часовой пояс населенного пункта
        /// </summary>
        [MaxLength(255)]
        public string TimeZone { get; set; }

        /// <summary>
        /// Ограничение на сумму наложенного платежа в населенном пункте
        /// </summary>
        public decimal PaymentLimit { get; set; }

        /// <summary>
        /// Список ошибок
        /// </summary>
        public List<Error> Errors { get; set; }
    }
}
