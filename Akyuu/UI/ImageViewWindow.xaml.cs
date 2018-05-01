using Akyuu.Data;
using Akyuu.Misc;
using Akyuu.Models;
using Akyuu.Models.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
using System.Windows.Shapes;

namespace Akyuu.UI {
    public partial class ImageViewWindow : Window {
        public Screenshot Screenshot { get; }

        public ObservableCollection<ScreenshotTag> Tags { get; }

        public ImageViewWindow(Screenshot screenshot) {
            InitializeComponent();
            DataContext = this;

            Screenshot = screenshot;

            using(var ctx = new AkyuuContext()) {
                var tags = from t in ctx.ScreenshotTags
                           where t.File == screenshot.File
                           orderby t.TagName
                           select t;
                Tags = new ObservableCollection<ScreenshotTag>(tags);
            }

            lsTags.Focus();
        }

        private void AddTags() {
            var window = new AddTagsWindow(Screenshot.File);
            if(window.ShowDialog(this) is true) {
                using(var ctx = new AkyuuContext()) {
                    ctx.ScreenshotTags.AddRange(window.Tags);

                    ctx.SaveChanges();
                }

                foreach(var tag in window.Tags) Tags.Add(tag);
            }
        }

        private void RemoveTags() {
            if(lsTags.SelectedItem is ScreenshotTag tag) {
                using(var ctx = new AkyuuContext()) {
                    ctx.ScreenshotTags.Attach(tag);
                    ctx.ScreenshotTags.Remove(tag);

                    ctx.SaveChanges();
                }

                Tags.Remove(tag);
            }
        }

        private void AddTags_Click(object sender, RoutedEventArgs e) {
            AddTags();
        }

        private void RemoveTags_Click(object sender, RoutedEventArgs e) {
            RemoveTags();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e) {
            switch(e.Key) {
            case Key.Escape:
                Close();
                break;
            case Key.N:
                AddTags();
                break;
            case Key.Delete:
                RemoveTags();
                break;
            }
        }
    }
}
