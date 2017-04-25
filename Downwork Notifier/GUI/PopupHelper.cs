using System;
using System.ComponentModel;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Downwork_Notifier.GUI
{
    public class PopupHelper : DependencyObject
    {
        #region Dependencey properties
        public String PopupTitle
        {
            get { return (String)GetValue(PopupTitleProperty); }
            set { SetValue(PopupTitleProperty, value); }
        }
        public static readonly DependencyProperty PopupTitleProperty =
            DependencyProperty.Register("PopupTitle", typeof(String), typeof(PopupHelper), new PropertyMetadata(null));

        public String PopupText
        {
            get { return (String)GetValue(PopupTextProperty); }
            set { SetValue(PopupTextProperty, value); }
        }
        public static readonly DependencyProperty PopupTextProperty =
            DependencyProperty.Register("PopupText", typeof(String), typeof(PopupHelper), new PropertyMetadata(String.Empty));


        public double PopupOpacity
        {
            get { return (double)GetValue(PopupOpacityProperty); }
            set { SetValue(PopupOpacityProperty, value); }
        }
        public static readonly DependencyProperty PopupOpacityProperty =
            DependencyProperty.Register("PopupOpacity", typeof(double), typeof(PopupHelper), new PropertyMetadata(0.85));


        public Brush PopupBgColor
        {
            get { return (Brush)GetValue(PopupBgColorProperty); }
            set { SetValue(PopupBgColorProperty, value); }
        }
        public static readonly DependencyProperty PopupBgColorProperty =
            DependencyProperty.Register("PopupBgColor", typeof(Brush), typeof(PopupHelper), new PropertyMetadata(Brushes.AliceBlue));
        #endregion Dependencey properties

        #region Privates
        private Popup _popupInstance = null;
        private Timer _timer = null;
        #endregion Privates

        #region Events
        public event EventHandler<MouseButtonEventArgs> DoubleClick;
        public event EventHandler<CancelEventArgs> Closing;
        public event EventHandler Closed;
        #endregion Events

        public PopupHelper() { }

        public Popup CreatePopup()
        {
            var bgGrid = new Grid();
            bgGrid.SetBinding(Grid.BackgroundProperty, new Binding("PopupBgColor")
            {
                Source = this,
                Mode = BindingMode.OneWay
            });
            bgGrid.SetBinding(Grid.OpacityProperty, new Binding("PopupOpacity")
            {
                Source = this,
                Mode = BindingMode.OneWay
            });

            var titleLbl = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontWeight = FontWeights.ExtraBold
            };
            titleLbl.SetBinding(TextBlock.TextProperty, new Binding("PopupTitle")
            {
                Source = this,
                Mode = BindingMode.OneWay
            });

            var mainTbx = new TextBox()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Margin = new Thickness(12, 0, 0, 0),
                Background = Brushes.Transparent,
                IsReadOnly = true,
                BorderThickness = new Thickness(0)
            };
            mainTbx.SetBinding(TextBox.TextProperty, new Binding("PopupText")
            {
                Source = this,
                Mode = BindingMode.OneWay
            });
            mainTbx.MouseDoubleClick += (_s, _e) => DoubleClick?.Invoke(this, _e);

            var scroll = new ScrollViewer()
            {
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                Content = mainTbx
            };

            var closeBtn = new Button()
            {
                Width = 50, Height = 50,
                Margin = new Thickness(25),
                VerticalAlignment = VerticalAlignment.Center,
                Background = Brushes.ForestGreen,
                Content = ResourceManager.Instance.Vectors["CheckWhite"]
            };
            closeBtn.Click += CloseBtn_Click;

            var mainGrid = new Grid();
            mainGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            mainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            Grid.SetRow(titleLbl, 0);
            Grid.SetColumn(titleLbl, 0);
            Grid.SetRow(scroll, 1);
            Grid.SetColumn(scroll, 0);
            Grid.SetRowSpan(closeBtn, 2);
            Grid.SetColumn(closeBtn, 1);
            Grid.SetRowSpan(bgGrid, 2);
            Grid.SetColumnSpan(bgGrid, 2);
            Canvas.SetZIndex(titleLbl, 100);
            Canvas.SetZIndex(scroll, 100);
            Canvas.SetZIndex(closeBtn, 100);
            Canvas.SetZIndex(bgGrid, 0);
            mainGrid.Children.Add(titleLbl);
            mainGrid.Children.Add(scroll);
            mainGrid.Children.Add(closeBtn);
            mainGrid.Children.Add(bgGrid);

            Popup popup = new Popup()
            {
                AllowsTransparency = true,
                Placement = PlacementMode.AbsolutePoint,
                PopupAnimation = PopupAnimation.Slide,
                StaysOpen = true,
                Height = 100,
                Width = SystemParameters.PrimaryScreenWidth
            };
            popup.Child = mainGrid;
            return popup;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            var closingArgs = new CancelEventArgs();
            Closing?.Invoke(this, closingArgs);
            if (!closingArgs.Cancel)
            {
                HidePopup();
                Closed?.Invoke(this, e);
            }
        }

        public void ShowPopup(String text, double timeToShow = 10000)
        {
            if (_popupInstance == null)
            {
                _popupInstance = CreatePopup();
            }

            PopupText = text;
            _popupInstance.IsOpen = true;

            if (_timer == null && timeToShow >= 0)
            {
                _timer = new Timer(timeToShow)
                {
                    AutoReset = false
                };
            }

            // Time to show == 0 to show popup indefinitely
            //
            if (timeToShow >= 0)
            {
                _timer.Elapsed += _timer_Elapsed;
                _timer.Start();
            }
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Application.Current?.Dispatcher?.Invoke(() =>
            {
                HidePopup();
            });
            if (_timer.Enabled)
            {
                _timer.Stop();
            }
        }

        public void HidePopup()
        {
            if (_popupInstance?.IsOpen ?? false)
            {
                _popupInstance.IsOpen = false;
            }
        }
    }
}
