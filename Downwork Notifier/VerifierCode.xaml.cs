using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Downwork_Notifier
{
    /// <summary>
    /// Interaction logic for VerifierCode.xaml
    /// </summary>
    public partial class VerifierCode : Window
    {
        public String OAuthVerifier
        {
            get { return (String)GetValue(OAuthVerifierProperty); }
            set { SetValue(OAuthVerifierProperty, value); }
        }
        // Using a DependencyProperty as the backing store for OAuthVerifier.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OAuthVerifierProperty =
            DependencyProperty.Register("OAuthVerifier", typeof(String), typeof(VerifierCode), new PropertyMetadata(String.Empty));



        public VerifierCode()
        {
            InitializeComponent();
        }
    }
}
