using ApiLibrary.ApiModules.Attributes;

namespace ApiLibrary.ApiEntities.Const
{
    public enum Workload
    {
        // as_needed, part_time, full_time
        None,
        [UrlParameter("as_needed")]
        AsNeeded,
        [UrlParameter("part_time")]
        PartTime,
        [UrlParameter("full_time")]
        FullTime
    }
}
