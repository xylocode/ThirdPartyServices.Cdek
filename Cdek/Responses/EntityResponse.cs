using System.Collections.Generic;
using XyloCode.ThirdPartyServices.Cdek.General;

namespace XyloCode.ThirdPartyServices.Cdek.Responses
{
    public class EntityResponse<T> : Response
        where T : class, new()
    {
        /// <summary>
        /// Информация о заказе
        /// </summary>
        public T Entity { get; set; }

        /// <summary>
        /// Информация о запросе над заказом
        /// </summary>
        public List<Request> Requests { get; set; }

        /// <summary>
        /// Связанные сущности (если в запросе был передан корректный print)
        /// </summary>
        public List<LinkedEntity> RelatedEntities { get; set; }

    }
}
