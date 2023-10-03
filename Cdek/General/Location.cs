using System;
using System.ComponentModel.DataAnnotations;

namespace XyloCode.ThirdPartyServices.Cdek.General
{
    /// <summary>
    /// Адрес местоположения контрагента (отправителя, получателя), включая геолокационные данные
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Код населенного пункта СДЭК (метод "Список населенных пунктов")
        /// </summary>
        public int? Code { get; set; }

        /// <summary>
        /// Уникальный идентификатор ФИАС
        /// </summary>
        public Guid? FiasGuid { get; set; }

        /// <summary>
        /// Почтовый индекс
        /// </summary>
        [MaxLength(255)]
        public string PostalCode { get; set; }

        /// <summary>
        /// Долгота
        /// </summary>
        public double? Longitude { get; set; }

        /// <summary>
        /// Широта
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        /// Код страны в формате ISO_3166-1_alpha-2
        /// </summary>
        [MaxLength(2)]
        public string CountryCode { get; set; }

        /// <summary>
        /// Название региона
        /// </summary>
        [MaxLength(255)]
        public string Region { get; set; }

        /// <summary>
        /// Название района
        /// </summary>
        [MaxLength(255)]
        public string SubRegion { get; set; }

        /// <summary>
        /// Название города
        /// </summary>
        [MaxLength(255)]
        public string City { get; set; }

        /// <summary>
        /// Код КЛАДР
        /// </summary>
        [Obsolete]
        [MaxLength(255)]
        public string KladrCode { get; set; }

        /// <summary>
        /// Строка адреса
        /// </summary>
        [MaxLength(255)]
        [Required]
        public string Address { get; set; }
    }
}
