using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Downwork_Notifier.ViewModels.API.ApiEntities.Profiles
{
    public partial class JobViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 JobViewModelId { get; set; }

        [Column("Url")]
        [Obsolete("That property is here for EF only - don't use it")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public String UrlEF
        {
            get { return Url.ToString(); }
            set { Url = value == null ? null : new Uri(value); }
        }

        [Column("Skills")]
        [Obsolete("That property is here for EF only - don't use it")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public String SkillsEF
        {
            get { return String.Join(";", _Skills); }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    _Skills.Clear();
                }
                else
                {
                    _Skills = new ObservableCollection<String>(value.Split(new[] { ';' }));
                }
            }
        }
    }
}
