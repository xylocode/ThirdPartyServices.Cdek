using System.Collections.Generic;
using XyloCode.ThirdPartyServices.Cdek.General;

namespace XyloCode.ThirdPartyServices.Cdek.Responses
{
    public class Response
    {
        public List<Error> Errors { get; set; }
        public List<Error> Warnings { get; set; }

    }
}
