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

        private IEnumerable<Screenshot> allFiles;

        public BrowsePage() {
            InitializeComponent();
            DataContext = this;

            allFiles = from t in Utils.ListImageFiles(Config.ScreenshotPath)
                       select Screenshot.FromFile(t);

            lsFiles.ItemsSource = allFiles;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {
            var text = (sender as TextBox).Text;

            lsFiles.ItemsSource = from t in allFiles
                                  where t.File.Contains(text)
                                  select t;
        }
    }
}
