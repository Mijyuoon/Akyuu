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
        public IEnumerable<TagInfo> Tags { get; }

        public TagListPage() {
            InitializeComponent();
            DataContext = this;

            using(var ctx = new AkyuuContext()) {
                Tags = (from t in ctx.ScreenshotTags
                       group t by t.TagName into g
                       orderby g.Key
                       select new TagInfo {
                           Name = g.Key,
                           Count = g.Count(),
                       }).ToList();
            }
        }
    }
}
