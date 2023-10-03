using System;
using System.ComponentModel.DataAnnotations;

namespace XyloCode.ThirdPartyServices.Cdek.General
{
    /// <summary>
    /// Информация о товарах места заказа (только для заказа типа "интернет-магазин")
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Наименование товара (может также содержать описание товара: размер, цвет)
        /// </summary>
        [MaxLength(255)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор/артикул товара
        /// </summary>
        [MaxLength(20)]
        [Required]
        public string WareKey { get; set; }

        /// <summary>
        /// Оплата за товар при получении (за единицу товара в указанной валюте, значение >=0) — наложенный платеж, в случае предоплаты значение = 0.
        /// </summary>
        [Required]
        public Money Payment { get; set; }

        /// <summary>
        /// Объявленная стоимость товара (за единицу товара в указанной валюте, значение >=0).
        /// С данного значения рассчитывается страховка
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Вес (за единицу товара, в граммах)
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// Вес брутто
        /// </summary>
        public int? WeightGross { get; set; }

        /// <summary>
        /// Количество единиц товара (в штуках)
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Наименование на иностранном языке
        /// </summary>
        [MaxLength(255)]
        public string NameI18n { get; set; }

        /// <summary>
        /// Бренд на иностранном языке
        /// </summary>
        [MaxLength(255)]
        public string Brand { get; set; }

        /// <summary>
        /// Код страны в формате  ISO_3166-1_alpha-2
        /// </summary>
        [MaxLength(2)]
        public string CountryCode { get; set; }

        /// <summary>
        /// Код материала
        /// </summary>
        [MaxLength(255)]
        public string Material { get; set; }

        /// <summary>
        /// Содержит wifi/gsm
        /// </summary>
        public bool? WifiGsm { get; set; }

        /// <summary>
        /// Ссылка на сайт интернет-магазина с описанием товара
        /// </summary>
        [MaxLength(255)]
        public string Url { get; set; }

        /// <summary>
        /// Информация по товарам в возвратном заказе (только для возвратного заказа)
        /// </summary>
        public string ReturnItemDetail { get; set; }


        /// <summary>
        /// Номер прямого заказа
        /// </summary>
        public string DirectOrderNumber { get; set; }


        /// <summary>
        /// UUID прямого заказа
        /// </summary>
        public Guid? DirectOrderUuid { get; set; }


        /// <summary>
        /// Номер упаковки товара в прямом заказе
        /// </summary>
        public string DirectPackageNumber { get; set; }

        /// <summary>
        /// Признак подакцизности товара
        /// </summary>
        public bool? Excise { get; set; }
    }
}
