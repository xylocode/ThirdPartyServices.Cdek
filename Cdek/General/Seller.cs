using System.ComponentModel.DataAnnotations;

namespace XyloCode.ThirdPartyServices.Cdek.General
{
    /// <summary>
    /// Данные истинного продавца
    /// </summary>
    public class Seller
    {
        /// <summary>
        /// Наименование истинного продавца
        /// </summary>
        [MaxLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// ИНН истинного продавца
        /// </summary>
        [MaxLength(20)]
        public string INN { get; set; }

        /// <summary>
        /// Телефон истинного продавца
        /// </summary>
        [MaxLength(255)]
        public string Phone { get; set; }

        /// <summary>
        /// Код формы собственности
        /// </summary>
        public int? OwnershipForm { get; set; }

        /// <summary>
        /// Адрес истинного продавца.
        /// Используется при печати инвойсов для отображения адреса настоящего продавца товара, либо торгового названия
        /// </summary>
        [MaxLength(255)]
        public string Address { get; set; }
    }
}
