using XyloCode.ThirdPartyServices.Cdek.Enums;

namespace XyloCode.ThirdPartyServices.Cdek.General
{
    /// <summary>
    /// Данные о дополнительных услугах
    /// </summary>
    public class Service
    {
        /// <summary>
        /// Код дополнительной услуги
        /// </summary>
        public ServiceCode Code { get; set; }

        /// <summary>
        /// Параметр дополнительной услуги
        /// </summary>
        public string Parameter { get; set; }
    }
}
