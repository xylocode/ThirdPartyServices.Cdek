using System;
using System.Collections.Generic;
using XyloCode.ThirdPartyServices.Cdek.Helpers;

namespace XyloCode.ThirdPartyServices.Cdek.Requests
{
    [IsQueryString]
    public class RegionsRequest
    {
        /// <summary>
        /// Массив кодов стран в формате  ISO_3166-1_alpha-2
        /// </summary>
        public List<string> CountryCodes { get; set; }

        /// <summary>
        /// Код региона СДЭК
        /// </summary>
        public int? RegionCode { get; set; }

        /// <summary>
        /// Код КЛАДР региона
        /// </summary>
        [Obsolete]
        public string KladrRegionCode { get; set; }


        /// <summary>
        /// Уникальный идентификатор ФИАС региона
        /// </summary>
        [Obsolete]
        public Guid? FiasRegionGuid { get; set; }

        /// <summary>
        /// Ограничение выборки результата. По умолчанию 1000
        /// </summary>
        public int? Size { get; set; }

        /// <summary>
        /// Номер страницы выборки результата. По умолчанию 0
        /// </summary>
        public int? Page { get; set; }

        /// <summary>
        /// Локализация офиса. По умолчанию "rus" (доступны eng и zho)
        /// </summary>
        public string Lang { get; set; }
    }
}
