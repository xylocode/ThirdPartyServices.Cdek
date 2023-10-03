using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using XyloCode.ThirdPartyServices.Cdek.General;

namespace XyloCode.ThirdPartyServices.Cdek.Models
{
    /// <summary>
    /// Регион СДЭК
    /// </summary>
    public class Region
    {
        /// <summary>
        /// Код страны в формате  ISO_3166-1_alpha-2
        /// </summary>
        [MaxLength(2)]
        [Required]
        public string CountryCode { get; set; }

        /// <summary>
        /// Название страны региона
        /// </summary>
        [MaxLength(255)]
        [Required]
        public string Country { get; set; }

        /// <summary>
        /// Название региона
        /// </summary>
        [MaxLength(255)]
        [Required]
        [JsonPropertyName("region")]
        public string Name { get; set; }

        /// <summary>
        /// Префикс региона
        /// </summary>
        [Obsolete]
        [MaxLength(255)]
        public string Prefix { get; set; }

        /// <summary>
        /// Код региона СДЭК
        /// </summary>
        public int? RegionCode { get; set; }

        /// <summary>
        /// Код КЛАДР региона
        /// </summary>
        [Obsolete]
        [MaxLength(255)]
        public string KladrRegionCode { get; set; }

        /// <summary>
        /// Уникальный идентификатор ФИАС региона
        /// </summary>
        [Obsolete]
        public Guid? FiasRegionGuid { get; set; }

        /// <summary>
        /// Список ошибок
        /// </summary>
        public List<Error> errors { get; set; }
    }
}
