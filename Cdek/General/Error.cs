using System.ComponentModel.DataAnnotations;

namespace XyloCode.ThirdPartyServices.Cdek.General
{
    /// <summary>
    /// Данные об ошибке обработки запроса на стороне ИС СДЭК
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Код ошибки
        /// </summary>
        [MaxLength(255)]
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// Описание ошибки
        /// </summary>
        [MaxLength(255)]
        [Required]
        public string Message { get; set; }
    }
}
