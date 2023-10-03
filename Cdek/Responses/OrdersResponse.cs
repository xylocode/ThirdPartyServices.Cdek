using System.Collections.Generic;

namespace XyloCode.ThirdPartyServices.Cdek.Responses
{
    public class OrdersResponse<T> : Response
    {
        public List<T> Orders { get; set; }
    }
}
