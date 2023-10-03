using System.Collections.Generic;
using XyloCode.ThirdPartyServices.Cdek.Enums;
using XyloCode.ThirdPartyServices.Cdek.General;

namespace XyloCode.ThirdPartyServices.Cdek.Requests
{
    public class BarcodeRequest
    {
        /// <summary>
        /// Список заказов
        /// </summary>
        public List<OrderId> Orders { get; set; }

        /// <summary>
        /// Число копий. По умолчанию 1
        /// </summary>
        public int? CopyCount { get; set; }

        /// <summary>
        /// Формат печати.
        /// Может принимать значения: A4, A5, A6 (A - буква латинского алфавита).
        /// По умолчанию A4
        /// </summary>
        public PaperSize? Format { get; set; }

        /// <summary>
        /// Язык печатной формы. Возможные языки в кодировке ISO - 639-3:
        /// Русский - RUS
        /// Английский - ENG
        /// </summary>
        public string Lang { get; set; }
    }
}
