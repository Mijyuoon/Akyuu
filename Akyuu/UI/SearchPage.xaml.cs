﻿using Akyuu.Data;
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
        private IEnumerable<TagInfo> allTags;

        public SearchPage() {
            InitializeComponent();
            DataContext = this;

            using(var ctx = AkyuuContext.Create()) {
                allTags = (from t in ctx.ScreenshotTags
                           group t by t.TagName into g
                           orderby g.Key
                           select new TagInfo {
                               Name = g.Key,
                           }).ToList();

                lsTagList.ItemsSource = allTags;
            }
        }

        private void TagList_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if(e.LeftButton != MouseButtonState.Pressed) return;
            e.Handled = true;

            var item = sender as ListBoxItem;
            if(item.Content is TagInfo data) {
                data.Selected = !data.Selected;
            }
        }

        private void TagSearch_Click(object sender, RoutedEventArgs e) {
            var tagName = from t in allTags
                          where t.Selected
                          select t.Name;
            int tagCount = tagName.Count();

            using(var ctx = AkyuuContext.Create()) {
                var files = from t in ctx.ScreenshotTags
                            where tagName.Contains(t.TagName)
                            group t by t.File into g
                            where g.Count() >= tagCount
                            select g.Key;

                if(cxSortDesc.IsChecked is true) {
                    files = from t in files
                            orderby t descending
                            select t;
                } else {
                    files = from t in files
                            orderby t ascending
                            select t;
                }

                lsBrowser.ItemsSource = from t in files.ToList()
                                        select Screenshot.FromFile(t);
            }
        }

        private void ShowUntagged_Click(object sender, RoutedEventArgs e) {
            using(var ctx = AkyuuContext.Create()) {
                var tagged = (from t in ctx.ScreenshotTags
                              select t.File).Distinct();

                var files = Utils.ListImageFiles(Config.Current.ScreenshotPath).Except(tagged);

                if(cxSortDesc.IsChecked is true) {
                    files = from t in files
                            orderby t descending
                            select t;
                } else {
                    files = from t in files
                            orderby t ascending
                            select t;
                }

                lsBrowser.ItemsSource = from t in files.ToList()
                                        select Screenshot.FromFile(t);
            }
        }

        private void TagsFilter_TextChanged(object sender, TextChangedEventArgs e) {
            var text = (sender as TextBox).Text;

            lsTagList.ItemsSource = from t in allTags
                                    where t.Selected || t.Name.ContainsCI(text)
                                    select t;
        }
    }
}
