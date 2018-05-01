using Akyuu.Data;
using Akyuu.Misc;
using Akyuu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Akyuu.UI {
    public partial class SearchPage : Page {
        public IEnumerable<TagInfo> Tags { get; }

        public SearchPage() {
            InitializeComponent();
            DataContext = this;

            using(var ctx = new AkyuuContext()) {
                Tags = (from t in ctx.ScreenshotTags
                        group t by t.TagName into g
                        orderby g.Key
                        select new TagInfo {
                            Name = g.Key,
                        }).ToList();
            }
        }

        private void TagList_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if(e.LeftButton != MouseButtonState.Pressed) return;

            var item = sender as ListBoxItem;
            if(item.Content is TagInfo data) {
                data.Selected = !data.Selected;
            }
        }

        private void TagSearch_Click(object sender, RoutedEventArgs e) {
            var tagName = from t in Tags
                          where t.Selected
                          select t.Name;
            int tagCount = tagName.Count();

            using(var ctx = new AkyuuContext()) {
                var files = from t in ctx.ScreenshotTags
                            where tagName.Contains(t.TagName)
                            group t by t.File into g
                            where g.Count() >= tagCount
                            select g.Key;

                lsBrowser.ItemsSource = from t in files.ToList()
                                        select Screenshot.FromFile(t);
            }
        }

        private void Browser_ItemDoubleClick(object sender, MouseButtonEventArgs e) {
            if(e.LeftButton != MouseButtonState.Pressed) return;

            var item = sender as ListBoxItem;
            if(item.Content is Screenshot data) {
                var window = new ImageViewWindow(data);
                window.ShowDialog(Application.Current.MainWindow);
            }
        }
    }
}
