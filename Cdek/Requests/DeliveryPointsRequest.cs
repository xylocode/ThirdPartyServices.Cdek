using System;
using XyloCode.ThirdPartyServices.Cdek.Enums;
using XyloCode.ThirdPartyServices.Cdek.Helpers;

namespace XyloCode.ThirdPartyServices.Cdek.Models
{
    [IsQueryString]
    public class DeliveryPointsRequest
    {
        /// <summary>
        /// Почтовый индекс города, для которого необходим список офисов
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Код населенного пункта СДЭК (метод "Список населенных пунктов")
        /// </summary>
        public int? CityCcode { get; set; }

        /// <summary>
        /// Тип офиса, может принимать значения:
        /// «PVZ» - для отображения складов СДЭК;
        /// «POSTAMAT» - для отображения постаматов СДЭК;
        /// «ALL» - для отображения всех ПВЗ независимо от их типа.
        /// При отсутствии параметра принимается значение по умолчанию «ALL».
        /// </summary>
        public DeliveryPointType? Type { get; set; }

        /// <summary>
        /// Код страны в формате ISO_3166-1_alpha-2 (см. “Общероссийский классификатор стран мира”)
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Код региона по базе СДЭК
        /// </summary>
        public int? RegionCode { get; set; }

        /// <summary>
        /// Наличие терминала оплаты
        /// </summary>
        public bool? HaveCashless { get; set; }

        /// <summary>
        /// Есть прием наличных
        /// </summary>
        public bool? HaveCash { get; set; }

        /// <summary>
        /// Разрешен наложенный платеж
        /// </summary>
        public bool? AllowedCod { get; set; }

        /// <summary>
        /// Наличие примерочной
        /// </summary>
        public bool? IsDressingRoom { get; set; }

        /// <summary>
        /// Максимальный вес в кг, который может принять офис (значения больше 0 - передаются офисы, которые принимают этот вес; 0 - офисы с нулевым весом не передаются; значение не указано - все офисы).
        /// </summary>
        public int? WeightMax { get; set; }

        /// <summary>
        /// Минимальный вес в кг, который принимает офис (при переданном значении будут выводиться офисы с минимальным весом до указанного значения)
        /// </summary>
        public int? WeightMin { get; set; }

        /// <summary>
        /// Локализация офиса. По умолчанию "rus" (доступны eng и zho)
        /// </summary>
        public string Lang { get; set; }

        /// <summary>
        /// Является ли офис только пунктом выдачи
        /// </summary>
        public bool? TakeOnly { get; set; }

        /// <summary>
        /// Является пунктом выдачи
        /// </summary>
        public bool? IsHandout { get; set; }

        /// <summary>
        /// Есть ли в офисе приём заказов
        /// </summary>
        public bool? IsReception { get; set; }

        /// <summary>
        /// Код города ФИАС
        /// </summary>
        public Guid? FiasGuid { get; set; }

        /// <summary>
        /// Код ПВЗ
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Работает ли офис с LTL (сборный груз)
        /// </summary>
        public bool? IsLtl { get; set; }

        /// <summary>
        /// Работает ли офис с "Фулфилмент. Приход"
        /// </summary>
        public bool? Fulfillment { get; set; }
    }
}
