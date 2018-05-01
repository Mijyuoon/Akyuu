using Akyuu.Misc;
using Akyuu.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class BrowsePage : Page {
        public Config Config => Config.Current;

        public IEnumerable<Screenshot> Images { get; }

        public BrowsePage() {
            InitializeComponent();
            DataContext = this;

            Images = from t in Utils.ListImageFiles(Config.ScreenshotPath)
                     select Screenshot.FromFile(t);
        }

        private void Browser_OpenScreenshot(object sender, Components.OpenScreenshotEventArgs e) {
            var window = new ImageViewWindow(e.Screenshot);
            window.ShowDialog(Application.Current.MainWindow);
        }
    }
}
