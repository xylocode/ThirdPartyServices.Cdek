using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XyloCode.ThirdPartyServices.Cdek.General
{
    /// <summary>
    /// Данные контрагента (отправителя, получателя)
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// Наименование компании
        /// </summary>
        [MaxLength(255)]
        public string Company { get; set; }

        /// <summary>
        /// Ф.И.О контактного лица
        /// </summary>
        [MaxLength(255)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Эл. адрес
        /// </summary>
        [MaxLength(255)]
        public string Email { get; set; }

        /// <summary>
        /// Список телефонов
        /// </summary>
        public List<Phone> Phones { get; set; }

        /// <summary>
        /// Серия паспорта
        /// </summary>
        [MaxLength(255)]
        public string PassportSeries { get; set; }

        /// <summary>
        /// Номер паспорта
        /// </summary>
        [MaxLength(255)]
        public string PassportNumber { get; set; }

        /// <summary>
        /// Дата выдачи паспорта
        /// </summary>
        public DateTime? PassportDateOfIssue { get; set; }

        /// <summary>
        /// Орган выдачи паспорта
        /// </summary>
        [MaxLength(255)]
        public string PassportOrganization { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime? PassportDateOfBirth { get; set; }

        /// <summary>
        /// Идентификационный номер налогоплательщика
        /// </summary>
        [MaxLength(255)]
        public string TIN { get; set; }

        /// <summary>
        /// Требования по паспортным данным удовлетворены (актуально для международных заказов):
        /// true - паспортные данные собраны или не требуются
        /// false - паспортные данные требуются и не собраны
        /// </summary>
        public bool? PassportRequirementsSatisfied { get; set; }
    }
}
