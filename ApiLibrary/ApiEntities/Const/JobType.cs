using ApiLibrary.ApiModules.Attributes;

namespace ApiLibrary.ApiEntities.Const
{
    public enum JobType
    {
        //hourly, fixed-price
        None,
        [UrlParameter("hourly")]
        Hourly,
        [UrlParameter("fixed")]
        FixedPrice
    }
}
