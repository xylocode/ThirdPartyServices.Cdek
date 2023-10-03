namespace XyloCode.ThirdPartyServices.Cdek.Enums
{
    public enum WebhookType : byte
    {
        /// <summary>
        /// событие по статусам
        /// </summary>
        ORDER_STATUS = 1,

        /// <summary>
        /// готовность печатной формы
        /// </summary>
        PRINT_FORM,

        /// <summary>
        /// получение фото документов по заказам
        /// </summary>
        DOWNLOAD_PHOTO,

        /// <summary>
        /// получение информации о закрытии преалерта
        /// </summary>
        PREALERT_CLOSED,
    }
}
