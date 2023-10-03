using XyloCode.ThirdPartyServices.Cdek.Enums;

namespace XyloCode.ThirdPartyServices.Cdek.Models
{
    public class Barcode : Print
    {
        /// <summary>
        /// Формат печати
        /// </summary>
        public PaperSize? Format { get; set; }

        /// <summary>
        /// Язык печатной формы в кодировке ISO - 639-3. По умолчанию - RUS.
        /// </summary>
        public string Lang { get; set; }
    }
}
