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
    public class OpenScreenshotEventArgs : EventArgs {
        public Screenshot Screenshot { get; }

        public OpenScreenshotEventArgs(Screenshot screenshot) {
            Screenshot = screenshot;
        }
    }

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

        public event EventHandler<OpenScreenshotEventArgs> OpenScreenshot;

        public ScreenshotBrowser() {
            InitializeComponent();
        }

        private void Browser_DoubleClick(object sender, MouseButtonEventArgs e) {
            if(e.LeftButton != MouseButtonState.Pressed) return;
            e.Handled = true;

            var item = sender as ListBoxItem;
            if(item.Content is Screenshot data) {
                OpenScreenshot?.Invoke(this, new OpenScreenshotEventArgs(data));
            }
        }

        private void Browser_KeyDown(object sender, KeyEventArgs e) {
            if(e.Key != Key.Enter) return;
            e.Handled = true;

            var item = sender as ListBoxItem;
            if(item.Content is Screenshot data) {
                OpenScreenshot?.Invoke(this, new OpenScreenshotEventArgs(data));
            }
        }
    }
}
