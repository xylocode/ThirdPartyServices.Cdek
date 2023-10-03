using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using XyloCode.ThirdPartyServices.Cdek.Enums;
using XyloCode.ThirdPartyServices.Cdek.General;

namespace XyloCode.ThirdPartyServices.Cdek.Models
{
    /// <summary>
    /// Офис СДЭК
    /// </summary>
    public class DeliveryPoint
    {
        /// <summary>
        /// Код
        /// </summary>
        [MaxLength(10)]
        public string Code { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        [Obsolete("Рекомендуется использовать параметры из массива \"location\"")]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор офиса в ИС СДЭК
        /// </summary>
        public Guid UUID { get; set; }

        /// <summary>
        /// Адрес офиса
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// Описание местоположения
        /// </summary>
        [MaxLength(255)]
        public string AddressComment { get; set; }

        /// <summary>
        /// Ближайшая станция/остановка транспорта
        /// </summary>
        [MaxLength(50)]
        public string NearestStation { get; set; }

        /// <summary>
        /// Ближайшая станция метро
        /// </summary>
        [MaxLength(50)]
        public string NearestMetroStation { get; set; }

        /// <summary>
        /// Режим работы, строка вида «пн-пт 9-18, сб 9-16»
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string WorkTime { get; set; }

        /// <summary
        /// Список телефонов
        /// </summary>
        [Required]
        public List<Phone> Phones { get; set; }

        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        [MaxLength(255)]
        public string Email { get; set; }

        /// <summary>
        /// Примечание по офису
        /// </summary>
        [MaxLength(50)]
        public string Note { get; set; }

        /// <summary>
        /// Тип ПВЗ
        /// </summary>
        public DeliveryPointType? Type { get; set; }

        /// <summary>
        /// Принадлежность офиса компании
        /// </summary>
        [MaxLength(255)]
        public string OwnerCode { get; set; }

        /// <summary>
        /// Является ли офис только пунктом выдачи или также осуществляет приём грузов
        /// </summary>
        public bool TakeOnly { get; set; }

        /// <summary>
        /// Является пунктом выдачи
        /// </summary>
        public bool IsHandout { get; set; }

        /// <summary>
        /// Является пунктом приёма
        /// </summary>
        public bool IsReception { get; set; }

        /// <summary>
        /// Есть ли примерочная
        /// </summary>
        public bool IsDressingRoom { get; set; }

        /// <summary>
        /// Есть безналичный расчет
        /// </summary>
        public bool HaveCashless { get; set; }

        /// <summary>
        /// Есть приём наличных
        /// </summary>
        public bool HaveCash { get; set; }

        /// <summary>
        /// Разрешен наложенный платеж в ПВЗ
        /// </summary>
        public bool AllowedCod { get; set; }

        /// <summary>
        /// Работает ли офис с LTL (сборный груз)
        /// </summary>
        public bool? IsLTL { get; set; }

        /// <summary>
        /// Работает ли офис с "Фулфилмент. Приход"
        /// </summary>
        public bool? Fulfillment { get; set; }

        /// <summary>
        /// Ссылка на данный офис на сайте СДЭК
        /// </summary>
        public string Site { get; set; }

        /// <summary>
        /// Все фото офиса (кроме фото как доехать)
        /// </summary>
        public List<Image> OfficeImageList { get; set; }

        /// <summary>
        /// График работы на неделю
        /// </summary>
        public List<WorkTime> WorkTimeList { get; set; }

        /// <summary>
        /// Исключения в графике работы офиса
        /// </summary>
        public List<WorkTimeException> WorkTimeExceptions { get; set; }

        /// <summary>
        /// Минимальный вес (в кг.), принимаемый в ПВЗ (> WeightMin)
        /// </summary>
        public float WeightMin { get; set; }

        /// <summary>
        /// Максимальный вес (в кг.), принимаемый в ПВЗ (<=WeightMax)
        /// </summary>
        public float WeightMax { get; set; }

        /// <summary>
        /// Перечень максимальных размеров ячеек (только для type = POSTAMAT)
        /// </summary>
        public List<PostomatDimensions> Dimensions { get; set; }

        /// <summary>
        /// Список ошибок
        /// </summary>
        public List<Error> Errors { get; set; }
    }
}
