using Akyuu.Data;
using Akyuu.UI.Components;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Akyuu.UI {
    public partial class SettingsPage : Page {
        public Models.Config Config => Models.Config.Current;

        public SettingsPage() {
            InitializeComponent();
            DataContext = this;
        }
    }
}
