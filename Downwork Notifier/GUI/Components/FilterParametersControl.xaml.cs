using Downwork_Notifier.Common;
using Downwork_Notifier.ViewModels;
using Downwork_Notifier.ViewModels.API.ApiEntities.Metadata;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Downwork_Notifier.GUI.Components
{
    /// <summary>
    /// Interaction logic for FilterParametersControl.xaml
    /// </summary>
    public partial class FilterParametersControl : UserControl
    {
        #region Constructor
        public FilterParametersControl()
        {
            InitializeComponent();
            DataContext = this;
        }
        #endregion Constructor


        #region Dependency properties
        public TabViewModel Tab
        {
            get { return (TabViewModel)GetValue(TabProperty); }
            set { SetValue(TabProperty, value); }
        }
        public static readonly DependencyProperty TabProperty =
            DependencyProperty.Register("Tab", typeof(TabViewModel), typeof(FilterParametersControl), new PropertyMetadata());


        public FilterableCollection<String> Skills
        {
            get { return (FilterableCollection<String>)GetValue(SkillsProperty); }
            set { SetValue(SkillsProperty, value); }
        }
        public static readonly DependencyProperty SkillsProperty =
            DependencyProperty.Register("Skills", typeof(FilterableCollection<String>), typeof(FilterParametersControl), new PropertyMetadata());


        public ObservableCollection<CategoryViewModel> Categories
        {
            get { return (ObservableCollection<CategoryViewModel>)GetValue(CategoriesProperty); }
            set { SetValue(CategoriesProperty, value); }
        }
        public static readonly DependencyProperty CategoriesProperty =
            DependencyProperty.Register("Categories", typeof(ObservableCollection<CategoryViewModel>), typeof(FilterParametersControl));


        public ICommand RefreshCommand
        {
            get { return (ICommand)GetValue(RefreshCommandProperty); }
            set { SetValue(RefreshCommandProperty, value); }
        }
        public static readonly DependencyProperty RefreshCommandProperty =
            DependencyProperty.Register("RefreshCommand", typeof(ICommand), typeof(FilterParametersControl), new PropertyMetadata(null));
        #endregion Dependency properties


        #region Event handlers
        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            var cbx = sender as ComboBox;
            if (cbx != null && Skills.FilteredCollection.Count > 1 && AddSkill(Skills.FilterValue))
            {
                Skills.FilterValue = String.Empty;
            }
        }

        private void ComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            var cbx = sender as ComboBox;
            if (cbx != null)
            {
                switch (e.Key)
                {
                    case Key.Enter:
                        if (AddSkill(Skills.FilterValue))
                        {
                            Skills.FilterValue = String.Empty;
                        }
                        e.Handled = true;
                        break;
                    case Key.Escape:
                        Skills.FilterValue = String.Empty;
                        e.Handled = true;
                        break;
                }
            }
        }

        private void ListView_KeyDown(object sender, KeyEventArgs e)
        {
            var lstBox = sender as ListView;
            if (lstBox != null && Tab.SelectedSkills.Contains((string)lstBox.SelectedValue))
            {
                switch (e.Key)
                {
                    case Key.Delete:
                    case Key.Back:
                        Tab.SelectedSkills.Remove((string)lstBox.SelectedValue);
                        e.Handled = true;
                        break;
                }
            }
        }
        #endregion Event handlers


        #region Methods
        public Boolean AddSkill(string skill)
        {
            if (Skills?.FilteredCollection != null && Skills.FilteredCollection.Contains(skill) && !Tab.SelectedSkills.Contains(skill))
            {
                Tab.SelectedSkills.Add(skill);
                return true;
            }
            return false;
        }
        #endregion Methods
    }
}
