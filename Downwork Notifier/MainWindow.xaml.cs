using Downwork_Notifier.Common;
using Downwork_Notifier.Common.Converters;
using Downwork_Notifier.ViewModels;
using Downwork_Notifier.ViewModels.API.ApiEntities.Profiles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Downwork_Notifier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Const
        public const string CT_DATE_DIFF = "mainWnd_NowDifferenceHoursConverter";
        public const string CT_JOB_STATUS_BRUSH = "mainWnd_JobStatusBrushConverter";
        #endregion Const

        #region DPs
        public MainPageViewModel DataModel
        {
            get { return (MainPageViewModel)GetValue(DataModelProperty); }
            set { SetValue(DataModelProperty, value); }
        }
        public static readonly DependencyProperty DataModelProperty =
            DependencyProperty.Register("DataModel", typeof(MainPageViewModel), typeof(MainWindow), new PropertyMetadata(new MainPageViewModel()));
        #endregion DPs

        #region privates
        private DispatcherTimer _timer = new DispatcherTimer();
        private System.Windows.Forms.NotifyIcon _tray = new System.Windows.Forms.NotifyIcon();
        #endregion privates

        public MainWindow()
        {
            InitializeComponent();

            #region Dynamic converters
            BindingHelper.Instance.RegisterConverter(CT_DATE_DIFF,
                new GenericConverter<DateTime, double>(dt => DateTime.Now.Subtract(dt).TotalHours));

            BindingHelper.Instance.RegisterConverter(CT_JOB_STATUS_BRUSH,
                new GenericConverter<String, Brush>(
                    js =>
                    {
                        if (js == ApiLibrary.ApiEntities.Const.JobStatus.Cancelled.ToString())
                        {
                            return Brushes.LightPink;
                        }
                        else if (js == ApiLibrary.ApiEntities.Const.JobStatus.Completed.ToString())
                        {
                            return Brushes.DarkOliveGreen;
                        }
                        else if (js == ApiLibrary.ApiEntities.Const.JobStatus.Open.ToString())
                        {
                            return Brushes.LightGreen;
                        }
                        else
                        {
                            return Brushes.Transparent;
                        }
                    }
                )
            );
            #endregion Dynamic converters

            // Restoring main window model from DB
            //
            var settings = App.DbContext.ApplicationSettings
                .Include(_ => _.Categories)
                .ThenInclude(_ => _.SubCategories)
                .FirstOrDefault();
            if (settings != null)
            {
                if (settings.Categories?.Any() ?? false)
                {
                    var ordered = settings.Categories.OrderBy(_ => _.Id);
                    settings.Categories = new ObservableCollection<ViewModels.API.ApiEntities.Metadata.CategoryViewModel>(ordered);
                }
                DataModel = settings;

                // Connecting
                //
                if (!String.IsNullOrWhiteSpace(DataModel.AccessToken) && !String.IsNullOrWhiteSpace(DataModel.AccessTokenSecret))
                {
                    DataModel.WindowStatus = Properties.Resources.MSG_ACCESS_TOKEN_FOUND;
                    DataModel.Authorize.Execute(null); // todo: here <-- it's async
                }

                // If app was hidden then hide it again
                //
                if (DataModel.IsMinimizedToTray)
                {
                    Hide();
                }
            }
            else
            {
                App.DbContext.Add(DataModel);
            }

            // If available skills was reset to null, then restore to empty collection
            //
            if (DataModel.AvailableSkills == null)
            {
                DataModel.AvailableSkills = new FilterableCollection<string>();
            }

            // Attaching filter delegate for skills
            //
            DataModel.AvailableSkills.PassFilterDelegate = (item, filter) =>
            {
                return item.StartsWith(filter, StringComparison.InvariantCultureIgnoreCase);
            };

            // Restoring tabs from DB
            //
            var tabs = App.DbContext.Tabs
                .Include(t => t.Jobs).ThenInclude(j => j.Client)
                .Include(t => t.Jobs)
                .Include(t => t.Filter)
                .Include(t => t.Filter.Budget)
                .Include(t => t.Filter.ClientFeedback)
                .Include(t => t.Filter.ClientHires)
                .Include(t => t.Filter.Paging)
                .ToList();
            foreach (var tab in tabs)
            {
                if (tab?.Jobs != null)
                {
                    tab.Jobs = new ObservableCollection<JobViewModel>(tab.Jobs.OrderByDescending(j => j.DateCreated));
                }
            }
            DataModel.Tabs = (tabs?.Any() ?? false) ? new ObservableCollection<TabViewModel>(tabs) : new ObservableCollection<TabViewModel>();

            // Handler for timer
            //
            _timer.Interval = new TimeSpan(0, 5, 0);
            _timer.Tick += _timer_Tick;

            // Handling datamodel changing
            //
            DataModel.PropertyChanged += DataModel_PropertyChanged;

            #region Tray
            _tray.Icon = System.Drawing.Icon.FromHandle(Properties.Resources.ICON_TRAY_DEACTIVATED.Handle);
            _tray.Text = Properties.Resources.APP_NAME;
            _tray.Visible = true;

            _tray.DoubleClick += delegate { DataModel.IsMinimizedToTray = false; };
            _tray.ContextMenu = new System.Windows.Forms.ContextMenu(new[] {
                new System.Windows.Forms.MenuItem(Properties.Resources.TRAY_OPEN, delegate { DataModel.IsMinimizedToTray = false; })
                { Name="Restore" },
                new System.Windows.Forms.MenuItem(Properties.Resources.TRAY_REFRESH, (_s, _e) => {
                    _timer_Tick(null, null);
                })
                { Name= "Refresh" },
                new System.Windows.Forms.MenuItem(Properties.Resources.POPUPS_ENABLE_LBL, (_s, _e) => {
                    DataModel.IsEnabledPopups = !DataModel.IsEnabledPopups;
                })
                { Name="EnablePopups" },
                new System.Windows.Forms.MenuItem("-"),
                new System.Windows.Forms.MenuItem(Properties.Resources.TRAY_CLOSE, (_s, _e) => {
                    _tray.Visible = false;
                    _tray.Dispose();
                    _tray = null;
                    Close();
                })
            });

            _tray.ContextMenu.Popup += (_s, _e) =>
            {
                var menuItem = _tray.ContextMenu.MenuItems.Find("Restore", false).FirstOrDefault();
                if (menuItem != null)
                {
                    menuItem.Enabled = DataModel.IsMinimizedToTray;
                }

                menuItem = _tray.ContextMenu.MenuItems.Find("Refresh", false).FirstOrDefault();
                if (menuItem != null)
                {
                    menuItem.Enabled = _timer.IsEnabled;
                }

                menuItem = _tray.ContextMenu.MenuItems.Find("EnablePopups", false).FirstOrDefault();
                if (menuItem != null)
                {
                    menuItem.Checked = DataModel.IsEnabledPopups;
                }
            };
            #endregion Tray

            DataContext = DataModel;
            Application.Current.Exit += OnExit;
        }

        private void OnExit(object sender, ExitEventArgs e)
        {
            if (App.DbContext != null)
            {
                DataModel.WindowStatus = "Saving changes...";
            }
        }


        #region Event handlers

        #region Tray related events
        protected override void OnClosing(CancelEventArgs e)
        {
            if (_tray != null)
            {
                e.Cancel = true;
                DataModel.IsMinimizedToTray = true;
            }
            else
            {
                _timer?.Stop();
            }

            base.OnClosing(e);
        }
        #endregion Tray related events


        #region Tab events
        private void newTab_Click(object sender, RoutedEventArgs e)
        {
            var newTab = new TabViewModel();
            //App.DbContext.Tabs.Add(newTab);
            DataModel.Tabs.Add(newTab);
            App.DbContext.SaveChanges();
        }

        private void editTab_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var panel = btn?.Parent as StackPanel;
            var tb = panel?.Children?[0] as TextBlock;
            if (tb != null)
            {
                var nameBindingExpression = tb.GetBindingExpression(TextBlock.TextProperty);
                if (nameBindingExpression != null)
                {
                    btn.Visibility = tb.Visibility = Visibility.Collapsed;

                    var tbx = new TextBox();
                    tbx.IsInactiveSelectionHighlightEnabled = true;
                    tbx.SetBinding(TextBox.TextProperty, nameBindingExpression.ParentBindingBase);

                    #region OnEdited
                    tbx.LostFocus += (_s, _e) =>
                    {
                        nameBindingExpression.UpdateSource();
                        panel.Children.Remove(tbx);
                        btn.Visibility = tb.Visibility = Visibility.Visible;
                    };
                    tbx.KeyDown += (_s, _e) =>
                    {
                        if (_e.Key == Key.Enter)
                        {
                            tbx.MoveFocus(new TraversalRequest(FocusNavigationDirection.Previous));
                        }
                    };
                    #endregion OnEdited

                    panel.Children.Insert(0, tbx);
                    //tbx.PreviewMouseLeftButtonDown += (_s, _e) =>
                    //{
                    //    var textbox = (_s as TextBox);
                    //    if (textbox != null && !textbox.IsKeyboardFocusWithin)
                    //    {
                    //        if (_e.OriginalSource.GetType().Name == "TextBoxView")
                    //        {
                    //            _e.Handled = true;
                    //            textbox.Focus();
                    //        }
                    //    }
                    //};
                    //tbx.GotKeyboardFocus += (_s, _e) =>
                    //{
                    //    //(_s as TextBox)?.SelectAll();
                    //    (_e.OriginalSource as TextBox)?.Select(1,3);
                    //};
                    tbx.Focus();
                }
            }
        }

        private async void trashTab_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var contentPresenter = btn?.TemplatedParent as ContentPresenter;
            var tab = contentPresenter?.DataContext as TabViewModel;
            if (tab != null)
            {
                App.DbContext.Jobs.RemoveRange(tab.Jobs);
                tab.Jobs.Clear();
                await App.DbContext.SaveChangesAsync();
            }
        }

        private async void removeTab_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var contentPresenter = btn?.TemplatedParent as ContentPresenter;
            var tab = contentPresenter?.DataContext as TabViewModel;
            if (tab != null)
            {
                App.DbContext.Remove(tab);
                DataModel.Tabs.Remove(tab);
                await App.DbContext.SaveChangesAsync();
            }
        }
        #endregion Tab events


        private void DataModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DataModel.IsMinimizedToTray))
            {
                ShowInTaskbar = !DataModel.IsMinimizedToTray;
                if (DataModel.IsMinimizedToTray)
                {
                    Hide();
                }
                else
                {
                    Show();
                    Focus();
                }
            }
        }

        private void startSearching_Click(object sender, RoutedEventArgs e)
        {
            if (!_timer.IsEnabled)
            {
                _tray.Icon = System.Drawing.Icon.FromHandle(Properties.Resources.ICON_TRAY_ACTIVE.Handle);
                _timer.Start();
            }
            _timer_Tick(null, null);
        }

        private async void _timer_Tick(object sender, EventArgs e)
        {
            var tabs = DataModel.Tabs.Where(_ => !String.IsNullOrWhiteSpace($"{_.Filter.Query}{_.Filter.Title}{_.Filter.Skills}")).ToArray();
            if (tabs.Length > 0)
            {
                foreach (var tab in tabs)
                {
                    await DataModel.RefreshJobs(tab);
                    await Task.Delay(1); // ?
                }
                await App.DbContext.SaveChangesAsync();
            }
        }

        private void DataGridRow_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var gridRow = sender as DataGridRow;
            if (gridRow != null && e.Source is System.Windows.Controls.Primitives.DataGridCellsPresenter)
            {
                gridRow.DetailsVisibility = gridRow.DetailsVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                e.Handled = true;
            }
        }

        private void DataGridRowHeader_Click(object sender, RoutedEventArgs e)
        {
            var rh = sender as System.Windows.Controls.Primitives.DataGridRowHeader;
            if (rh?.ToolTip is Uri)
            {
                System.Diagnostics.Process.Start(((Uri)rh.ToolTip).AbsoluteUri);
                e.Handled = true;
            }
        }
        #endregion Event handlers
    }
}
