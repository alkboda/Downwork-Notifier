using ApiLibrary.ApiModules.Attributes;

namespace ApiLibrary.ApiEntities.Const
{
    public enum JobStatus
    {
        //open, completed, cancelled
        None,
        [UrlParameter("open")]
        Open,
        [UrlParameter("completed")]
        Completed,
        [UrlParameter("cancelled")]
        Cancelled
    }
}
