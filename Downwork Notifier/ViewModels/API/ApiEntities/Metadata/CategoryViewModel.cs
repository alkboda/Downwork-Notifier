using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Downwork_Notifier.ViewModels.API.ApiEntities.Metadata
{
    public partial class CategoryViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 CategoryViewModelId { get; set; }
    }
}
