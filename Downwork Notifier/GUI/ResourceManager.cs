using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Downwork_Notifier.GUI
{
    public class ResourceManager
    {
        private static ResourceManager _instance = null;
        private ResourceManager() { }
        public static ResourceManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ResourceManager();
                }
                return _instance;
            }
        }

        public ResourceDictionary Vectors { get; } = new ResourceDictionary() { Source = new Uri("/GUI/Vector.xaml", UriKind.Relative) };
    }
}
