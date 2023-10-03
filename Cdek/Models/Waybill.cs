using XyloCode.ThirdPartyServices.Cdek.Enums;

namespace XyloCode.ThirdPartyServices.Cdek.Models
{
    public class Waybill : Print
    {
        /// <summary>
        /// Форма квитанции
        /// </summary>
        public WaybillType? Type { get; set; }
    }
}
