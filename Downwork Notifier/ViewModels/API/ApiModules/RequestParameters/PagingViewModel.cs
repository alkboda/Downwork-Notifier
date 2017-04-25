using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Downwork_Notifier.ViewModels.API.ApiModules.RequestParameters
{
    public partial class PagingViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 PagingViewModelId { get; set; }

        public override string ToString()
        {
            if (Offset == 0 && Count == 0)
            {
                return "0;100";
            }
            return $"{Offset};{Count}";
        }
    }
}
