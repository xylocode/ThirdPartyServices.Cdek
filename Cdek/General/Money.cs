namespace XyloCode.ThirdPartyServices.Cdek.General
{
    /// <summary>
    /// Стоимость услуги/товара с учетом налогообложения
    /// </summary>
    public class Money
    {
        /// <summary>
        /// Сумма в валюте
        /// </summary>
        public decimal Value { get; set; }

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
