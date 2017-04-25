using Downwork_Notifier.ViewModels.API.ApiEntities.Profiles;
using Downwork_Notifier.ViewModels.API.ApiModules.RequestParameters;
using Downwork_Notifier.ViewModels.Common;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Downwork_Notifier.ViewModels
{
    public class TabViewModel : BindableBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 TabViewModelId { get; set; }

        public String FilterName
        {
            get { return _filterName; }
            set { SetProperty(ref _filterName, value); }
        }
        private string _filterName = Properties.Resources.FILTER_NEW_FILTER_NAME;

        public Boolean SettingsExpanded
        {
            get { return _settingsExpanded; }
            set { SetProperty(ref _settingsExpanded, value); }
        }
        private bool _settingsExpanded = false;

        public JobSearchParametersViewModel Filter
        {
            get { return _filter; }
            set { SetProperty(ref _filter, value); }
        }
        private JobSearchParametersViewModel _filter = new JobSearchParametersViewModel();

        public ObservableCollection<JobViewModel> Jobs
        {
            get { return _jobs; }
            set { SetProperty(ref _jobs, value); }
        }
        private ObservableCollection<JobViewModel> _jobs = new ObservableCollection<JobViewModel>();

        #region Selected skills
        [NotMapped]
        public ObservableCollection<String> SelectedSkills
        {
            get { return _selectedSkills; }
            set { SetProperty(ref _selectedSkills, value); }
        }
        private ObservableCollection<String> _selectedSkills = new ObservableCollection<string>();

        [Column("SelectedSkills")]
        [Obsolete("That property is here for EF only - don't use it")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public String SelectedSkillsEF
        {
            get { return String.Join(";", SelectedSkills); }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    _selectedSkills.Clear();
                }
                else
                {
                    _selectedSkills = new ObservableCollection<string>(value.Split(new[] { ';' }));
                }
            }
        }
        #endregion Selected skills
    }
}
