using System.Collections.Generic;
using XyloCode.ThirdPartyServices.Cdek.Models;

namespace XyloCode.ThirdPartyServices.Cdek.Responses
{
    public class ReceiptResponse : Response
    {
        public List<Receipt> CheckInfo { get; set; }
    }
}
