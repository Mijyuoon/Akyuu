using Akyuu.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Akyuu.UI.Components {
    public partial class ScreenshotBrowser : UserControl {
        public IEnumerable<Screenshot> ItemsSource {
            get { return (IEnumerable<Screenshot>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable<Screenshot>), typeof(ScreenshotBrowser));

        public int ThumbnailSize {
            get { return (int)GetValue(ThumbnailSizeProperty); }
            set { SetValue(ThumbnailSizeProperty, value); }
        }
        
        public static readonly DependencyProperty ThumbnailSizeProperty =
            DependencyProperty.Register("ThumbnailSize", typeof(int), typeof(ScreenshotBrowser), new PropertyMetadata(64));

        public event MouseButtonEventHandler ItemDoubleClick;

        public ScreenshotBrowser() {
            InitializeComponent();
        }

        private void Browser_DoubleClick(object sender, MouseButtonEventArgs e) {
            ItemDoubleClick?.Invoke(sender, e);
        }
    }
}
