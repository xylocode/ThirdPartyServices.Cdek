using System.ComponentModel.DataAnnotations;

namespace XyloCode.ThirdPartyServices.Cdek.General
{
    /// <summary>
    /// Номер телефона (мобильный/городской)
    /// </summary>
    public class Phone
    {
        /// <summary>
        /// Номер телефона
        /// Необходимо передавать в международном формате: код страны(для России +7) и сам номер(10 и более цифр)
        /// </summary>
        [MaxLength(255)]
        [Required]
        public string Number { get; set; }

        /// <summary>
        /// Дополнительная информация (добавочный номер)
        /// </summary>
        [MaxLength(255)]
        public string Additional { get; set; }
    }
}
