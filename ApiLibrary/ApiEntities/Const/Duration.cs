using ApiLibrary.ApiModules.Attributes;

namespace ApiLibrary.ApiEntities.Const
{
    public enum Duration
    {
        // week, month, quarter, semester, ongoing
        None,
        [UrlParameter("week")]
        Week,
        [UrlParameter("month")]
        Month,
        [UrlParameter("quarter")]
        Quarter,
        [UrlParameter("semester")]
        Semester,
        [UrlParameter("ongoing")]
        Ongoing
    }
}
