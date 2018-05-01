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

            using(var ctx = AkyuuContext.Create()) {
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

        private void Browser_OpenScreenshot(object sender, Components.OpenScreenshotEventArgs e) {
            var window = new ImageViewWindow(e.Screenshot);
            window.ShowDialog(Application.Current.MainWindow);
        }

        private void TagSearch_Click(object sender, RoutedEventArgs e) {
            var tagName = from t in Tags
                          where t.Selected
                          select t.Name;
            int tagCount = tagName.Count();

            using(var ctx = AkyuuContext.Create()) {
                var files = from t in ctx.ScreenshotTags
                            where tagName.Contains(t.TagName)
                            group t by t.File into g
                            where g.Count() >= tagCount
                            select g.Key;

                lsBrowser.ItemsSource = from t in files.ToList()
                                        select Screenshot.FromFile(t);
            }
        }

        private void ShowUntagged_Click(object sender, RoutedEventArgs e) {
            var files = Utils.ListImageFiles(Config.Current.ScreenshotPath);

            using(var ctx = AkyuuContext.Create()) {
                var tagged = (from t in ctx.ScreenshotTags
                              select t.File).Distinct();

                lsBrowser.ItemsSource = from t in files.Except(tagged)
                                        select Screenshot.FromFile(t);
            }
        }
    }
}
