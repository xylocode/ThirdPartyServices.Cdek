using System;
using System.Collections.Generic;
using XyloCode.ThirdPartyServices.Cdek.Helpers;

namespace XyloCode.ThirdPartyServices.Cdek.Requests
{
    [IsQueryString]
    public class CitiesRequest
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
        /// Код КЛАДР населенного пункта
        /// </summary>
        [Obsolete]
        public string KladrCode { get; set; }

        /// <summary>
        /// Уникальный идентификатор ФИАС населенного пункта
        /// </summary>
        public Guid? FiasGuid { get; set; }

        /// <summary>
        /// Почтовый индекс
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Код населенного пункта СДЭК
        /// </summary>
        public int? Code { get; set; }

        /// <summary>
        /// Название населенного пункта. Должно соответствовать полностью
        /// </summary>
        public string City { get; set; }

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

        /// <summary>
        /// Ограничение на сумму наложенного платежа:
        /// -1 - ограничения нет;
        /// 0 - наложенный платеж не принимается;
        /// </summary>
        public decimal? PaymentLimit { get; set; }
    }
}
