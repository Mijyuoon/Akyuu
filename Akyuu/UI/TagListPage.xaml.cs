using Akyuu.Data;
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

namespace Akyuu.UI {
    public partial class TagListPage : Page {
        private IEnumerable<TagInfo> allTags;

        public TagListPage() {
            InitializeComponent();
            DataContext = this;

            using(var ctx = AkyuuContext.Create()) {
                allTags = (from t in ctx.ScreenshotTags
                           group t by t.TagName into g
                           orderby g.Key
                           select new TagInfo {
                               Name = g.Key,
                               Count = g.Count(),
                           }).ToList();
            }

            lsTags.ItemsSource = allTags;
        }

        private void TagFilter_TextChanged(object sender, TextChangedEventArgs e) {
            var text = (sender as TextBox).Text;

            lsTags.ItemsSource = from t in allTags
                                 where t.Name.Contains(text)
                                 select t;
        }
    }
}
