using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XyloCode.ThirdPartyServices.Cdek.General
{
    /// <summary>
    /// Информация о местах заказа
    /// </summary>
    public class Package
    {
        /// <summary>
        /// Номер упаковки (можно использовать порядковый номер упаковки заказа или номер заказа), уникален в пределах заказа.
        /// Идентификатор заказа в ИС Клиента
        /// </summary>
        [MaxLength(255)]
        [Required]
        public string Number { get; set; }

        /// <summary>
        /// Общий вес (в граммах)
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// Габариты упаковки. Длина (в сантиметрах)
        /// </summary>
        public int? Length { get; set; }

        /// <summary>
        /// Габариты упаковки. Ширина (в сантиметрах)
        /// </summary>
        public int? Width { get; set; }

        /// <summary>
        /// Габариты упаковки. Высота (в сантиметрах)
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        /// Комментарий к упаковке
        /// </summary>
        [MaxLength(255)]
        public string Comment { get; set; }

        /// <summary>
        /// Позиции товаров в упаковке
        /// </summary>
        public List<Item> Items { get; set; }
    }
}
