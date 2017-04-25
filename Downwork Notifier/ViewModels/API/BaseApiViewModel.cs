using Downwork_Notifier.ViewModels.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using static Utilities.Extensions.TypeEx;

namespace Downwork_Notifier.ViewModels.API
{
    public abstract class BaseApiViewModel<TApiEntity> : BindableBase where TApiEntity : class
    {
        public BaseApiViewModel() : this((TApiEntity)typeof(TApiEntity).CreateDefaultInstance()) { }
        public BaseApiViewModel(TApiEntity entity)
        {
            Entity = entity ?? throw new ArgumentNullException(nameof(entity));
        }

        [NotMapped]
        public virtual TApiEntity Entity { get; private set; }
    }
}
