using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Downwork_Notifier.ViewModels.API.ApiModules.RequestParameters
{
    public partial class RangeParameterViewModel<TValue>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 RangeParameterViewModelId { get; set; }

        public override string ToString()
        {
            if (Min == null && Max == null)
            {
                return null;
            }

            return $"{Min}-{Max}";
        }
    }
}
