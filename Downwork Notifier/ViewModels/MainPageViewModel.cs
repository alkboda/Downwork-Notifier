using Downwork_Notifier.API;
using Downwork_Notifier.Common;
using Downwork_Notifier.Common.Comparers;
using Downwork_Notifier.GUI;
using Downwork_Notifier.GUI.Commands;
using Downwork_Notifier.ViewModels.API.ApiEntities.Metadata;
using Downwork_Notifier.ViewModels.API.ApiEntities.Profiles;
using Downwork_Notifier.ViewModels.Common;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Downwork_Notifier.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        #region private fields
        private ApiHelper _apiHelper = null;
        #endregion private fields


        #region Constructors
        //public MainPageViewModel() { }
        //~MainPageViewModel()
        //{
        //    if (_dbContext != null)
        //    {
        //        _dbContext.Dispose();
        //        _dbContext = null;
        //    }
        //}
        #endregion Constructors


        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 MainPageViewModelId { get; set; }

        public String AccessToken
        {
            get { return _accessToken; }
            set { SetProperty(ref _accessToken, value); }
        }
        private string _accessToken = String.Empty;

        public String AccessTokenSecret
        {
            get { return _accessTokenSecret; }
            set { SetProperty(ref _accessTokenSecret, value); }
        }
        private string _accessTokenSecret = String.Empty;

        [NotMapped]
        public Boolean IsConnected
        {
            get { return _isConnected; }
            set { SetProperty(ref _isConnected, value); }
        }
        private bool _isConnected = false;

        public Boolean IsMinimizedToTray
        {
            get { return _isMinimizedToTray; }
            set
            {
                _isMinimizedToTray = value;
                OnPropertyChanged(nameof(IsMinimizedToTray)); // refresh each time
            }
        }
        private bool _isMinimizedToTray = false;

        [NotMapped]
        public String WindowStatus
        {
            get { return _windowStatus; }
            set { SetProperty(ref _windowStatus, value); }
        }
        private string _windowStatus = String.Empty;

        public Boolean IsEnabledPopups
        {
            get { return _isEnabledPopups; }
            set
            {
                if (SetProperty(ref _isEnabledPopups, value) && value && PopupDuration == 0)
                {
                    PopupDuration = 15 * 60;
                }
            }
        }
        private bool _isEnabledPopups = true;

        public Int32 PopupDuration
        {
            get { return _popupDuration; }
            set
            {
                if (SetProperty(ref _popupDuration, value) && value == 0)
                {
                    IsEnabledPopups = false;
                }
            }
        }
        private int _popupDuration = 15 * 60;

        public ObservableCollection<TabViewModel> Tabs
        {
            get { return _tabs; }
            set
            {
                var oldCollection = _tabs;
                if (SetProperty(ref _tabs, value))
                {
                    if (oldCollection != null)
                    {
                        oldCollection.CollectionChanged -= Tabs_CollectionChanged;
                    }
                    if (_tabs != null)
                    {
                        _tabs.CollectionChanged += Tabs_CollectionChanged;
                    }
                }
            }
        }
        private void Tabs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewStartingIndex <= TabIndex)
            {
                OnPropertyChanged(nameof(CurrentTab));
            }
        }
        private ObservableCollection<TabViewModel> _tabs = null;

        public Int16 TabIndex
        {
            get { return _tabIndex; }
            set
            {
                if (SetProperty(ref _tabIndex, value))
                {
                    OnPropertyChanged(nameof(CurrentTab));
                }
            }
        }
        private short _tabIndex = 0;
        
        public TabViewModel CurrentTab
        {
            get { return ((Tabs?.Count ?? 0) > TabIndex && TabIndex >= 0) ? Tabs[TabIndex] : null; }
        }

        #region Available Skills
        [NotMapped]
        public FilterableCollection<String> AvailableSkills
        {
            get { return _availableSkills; }
            set { SetProperty(ref _availableSkills, value); }
        }
        private FilterableCollection<String> _availableSkills = null;

        [Column("AvailableSkills")]
        [Obsolete("That property is here for EF only - don't use it")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public String AvailableSkillsEF
        {
            get { return String.Join(";", _availableSkills?.UnfilteredCollection ?? new string[0]); }
            set
            {
                if (_availableSkills == null)
                {
                    _availableSkills = new FilterableCollection<string>();
                }
                _availableSkills.UnfilteredCollection = value?.Split(new[] { ';' });
            }
        }
        #endregion Available Skills

        public ObservableCollection<CategoryViewModel> Categories
        {
            get { return _categories; }
            set { SetProperty(ref _categories, value); }
        }
        private ObservableCollection<CategoryViewModel> _categories = null;
        #endregion Properties


        #region Commands
        private ICommand _authorize = null;
        public ICommand Authorize
        {
            get
            {
                if (_authorize == null)
                {
                    _authorize = new CommandHandler(async (parameter) =>
                    {
                        WindowStatus = Properties.Resources.MSG_API_INITIALIZE;
                        _apiHelper = new ApiHelper(AccessToken, AccessTokenSecret);

                        WindowStatus = Properties.Resources.MSG_API_TRY_AUTHORIZE;
                        Tuple<string, string> tokens = null;
                        try
                        {
                            tokens = await _apiHelper.Init();
                        }
                        catch { }
                        if (tokens == null)
                        {
                            WindowStatus = Properties.Resources.MSG_API_AUTHORIZE_FAIL;
                            IsConnected = false;
                        }
                        else
                        {
                            WindowStatus = Properties.Resources.MSG_API_AUTHORIZE_SUCCESS;
                            AccessToken = tokens.Item1;
                            AccessTokenSecret = tokens.Item2;
                            IsConnected = true;

                            await App.DbContext.SaveChangesAsync();
                        }
                    });
                }
                return _authorize;
            }
        }

        private ICommand _refresh = null;
        public ICommand Refresh
        {
            get
            {
                if (_refresh == null)
                {
                    _refresh = new CommandHandler(async (parameter) =>
                    {
                        WindowStatus = Properties.Resources.MSG_METADATA_LOADING;
                        await _apiHelper.LoadMetadata();

                        WindowStatus = Properties.Resources.MSG_METADATA_LOADED
                            .Replace("{CATEGORIES_CNT}", _apiHelper.Categories.Length.ToString())
                            .Replace("{SKILLS_CNT}", _apiHelper.Skills.Length.ToString());

                        AvailableSkills.UnfilteredCollection = _apiHelper.Skills;
                        Categories = new ObservableCollection<CategoryViewModel>(_apiHelper.Categories);
                    });
                }
                return _refresh;
            }
        }
        #endregion Commands


        #region Methods
        public async Task RefreshJobs(TabViewModel tab) // todo -> concurrent run
        {
            var jobDtos = await _apiHelper.LoadJobs(tab.Filter);
            var jobs = jobDtos?.Select(_ => new JobViewModel(_));

            if (jobs != null && jobs.Any())
            {
                #region New jobs handling
                // Taking new jobs to show em in popup
                //
                var jobIdComparer = new GenericComparer<JobViewModel>(
                    (x, y) => (x?.Id == y?.Id),
                    obj => obj.Id.GetHashCode()
                );
                var newJobs = jobs.Except(tab.Jobs, jobIdComparer).ToArray();

                if (newJobs.Any())
                {
                    int insertIndex = 0;
                    foreach (var job in newJobs)
                    {
                        // Adding new jobs to the datagrid
                        //
                        tab.Jobs.Insert(insertIndex++, job);
                    }

                    // Building popup when needed
                    //
                    if (IsEnabledPopups)
                    {
                        var popupLines = newJobs.Select(job => String.Format("{0} [{1} – {2} / {3} / {4} / {5}]: {6}",
                            job.DateCreated,
                            job.JobType == ApiLibrary.ApiEntities.Profiles.JobType.Fixed ? job.Budget.ToString() : job.Workload,
                            job.JobType,
                            job.JobStatus,
                            job.Duration,
                            job.Client?.Country ?? "no country",
                            job.Title));
                        var popupText = String.Join(Environment.NewLine, popupLines);

                        var popupHelper = new PopupHelper();
                        popupHelper.PopupTitle = $"{Properties.Resources.MSG_POPUP_JOB_FOUND} [{tab.FilterName}]:";
                        popupHelper.ShowPopup(popupText, PopupDuration * 1000);

                        #region Popup dblclick handling
                        popupHelper.DoubleClick += (_s, _e) =>
                        {
                            // Restore and focus window
                            //
                            IsMinimizedToTray = false;

                            var _popupHelper = _s as PopupHelper;
                            if (_popupHelper != null)
                            {
                                // Set active tab for which popup was clicked
                                //
                                var title = _popupHelper.PopupTitle;
                                title = title?.Substring(title.LastIndexOf('[') + 1); // Extract filter name from title
                                title = title?.Substring(0, title.LastIndexOf(']'));
                                if (!String.IsNullOrWhiteSpace(title))
                                {
                                    var found = Tabs.FirstOrDefault(_ => _.FilterName == title);
                                    if (found != null)
                                    {
                                        // Set as current tab
                                        //
                                        TabIndex = (short)Tabs.IndexOf(found);
                                        found.SettingsExpanded = false;
                                    }
                                }

                                // Hide popup
                                //
                                _popupHelper.HidePopup();
                            }
                        };
                        #endregion Popup dblclick handling
                    }
                }
                #endregion New jobs handling

                #region Modified jobs handling
                // Taking changed jobs to update info in datagrid
                //
                var jobInfoComparer = new GenericComparer<JobViewModel>(
                    (x, y) =>
                    {
                        if (x?.Id != y.Id)
                        {
                            return false;
                        }
                        if (x.Budget != y.Budget
                            || x.Duration != y.Duration
                            || x.JobStatus != y.JobStatus
                            || x.JobType != y.JobType
                            || x.Title != y.Title
                            || x.Workload != y.Workload
                            || x.Url != y.Url
                        )
                        {
                            // return true for jobs with the same Id, but different info to catch through intersect
                            //
                            return true;
                        }
                        return false;
                    },
                    obj => obj.Id.GetHashCode()
                );
                var modifiedJobs = tab.Jobs.Intersect(jobs, jobInfoComparer).ToArray();

                if (modifiedJobs.Any())
                {
                    // Update info in datagrid
                    //
                    foreach (var job in modifiedJobs)
                    {
                        var changedJob = jobs.Single(_ => _.Id == job.Id);
                        job.Budget = changedJob.Budget;
                        job.Duration = changedJob.Duration;
                        job.JobStatus = changedJob.JobStatus;
                        job.JobType = changedJob.JobType;
                        job.Title = changedJob.Title;
                        job.Workload = changedJob.Workload;
                        job.Url = changedJob.Url;
                    }
                }
                #endregion Modified jobs handling
            }
        }
        #endregion Methods
    }
}
