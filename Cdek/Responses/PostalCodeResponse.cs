using System.Collections.Generic;

namespace XyloCode.ThirdPartyServices.Cdek.Responses
{
    /// <summary>
    /// Почтовые индексы СДЭК
    /// </summary>
    public class PostalCodeResponse
    {
        /// <summary>
        /// Код города, которому принадлежат почтовые индексы.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Почтовые индексы города
        /// </summary>
        public List<string> PostalCodes { get; set; }
    }
}
