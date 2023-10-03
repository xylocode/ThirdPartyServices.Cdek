using System.ComponentModel.DataAnnotations;
using XyloCode.ThirdPartyServices.Cdek.General;

namespace XyloCode.ThirdPartyServices.Cdek.Models
{
    public class OrderPhotoLink : OrderId
    {
        /// <summary>
        /// Ссылка на скачивание архива с фотографиями
        /// </summary>
        [MaxLength(1024)]
        public string Link { get; set; }
    }
}
