using System;

namespace XyloCode.ThirdPartyServices.Cdek.Helpers
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    sealed class IsQueryStringAttribute : Attribute
    {
        public IsQueryStringAttribute() { }
    }
}
