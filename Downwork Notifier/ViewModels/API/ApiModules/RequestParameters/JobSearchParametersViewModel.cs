using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Downwork_Notifier.ViewModels.API.ApiModules.RequestParameters
{
    public partial class JobSearchParametersViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 JobSearchParametersViewModelId { get; set; }
    }
}
