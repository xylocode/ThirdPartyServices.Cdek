using System.Text.Json.Serialization;

namespace XyloCode.ThirdPartyServices.Cdek.General
{
    /// <summary>
    /// Дополнительный сбор за доставку в зависимости от стоимости товара
    /// </summary>
    public class Threshold
    {
        /// <summary>
        /// Порог стоимости товара (действует по условию меньше или равно) в целых единицах валюты
        /// </summary>
        [JsonPropertyName("threshold")]
        public int ThresholdValue { get; set; }

        /// <summary>
        /// Доп. сбор за доставку товаров, общая стоимость которых попадает в интервал
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Сумма НДС
        /// </summary>
        public decimal? VatSum { get; set; }

        /// <summary>
        /// Ставка НДС (значение - 0, 10, 12, 20, null - нет НДС)
        /// </summary>
        public float? VatRate { get; set; }
    }
}
