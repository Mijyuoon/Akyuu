using Akyuu.Misc;
using Akyuu.Models.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public partial class AddTagsWindow : Window {
        public ICollection<ScreenshotTag> Tags { get; }

        public string File { get; }

        public AddTagsWindow(string file) {
            InitializeComponent();
            DataContext = this;

            File = file;
            Tags = new ObservableCollection<ScreenshotTag>();

            txTag.Focus();
        }

        private void AddNewTag() {
            if(txTag.Text.Length > 0) {
                Tags.Add(new ScreenshotTag {
                    File = File,
                    TagName = txTag.Text
                });

                txTag.Clear();
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e) {
            AddNewTag();
        }

        private void Entry_KeyDown(object sender, KeyEventArgs e) {
            if(e.Key == Key.Enter) {
                AddNewTag();
                e.Handled = true;
            }

            if(e.Key == Key.D && e.HasModifier(ModifierKeys.Control)) {
                Tags.Remove(Tags.Last());
                e.Handled = true;
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e) {
            if(Tags.Count < 1) return;

            var result = MessageBox.Show("Save the newly added tags?", "Confirm", 
                MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            switch(result) {
            case MessageBoxResult.Yes:
                DialogResult = true;
                break;
            case MessageBoxResult.No:
                DialogResult = false;
                break;
            case MessageBoxResult.Cancel:
                e.Cancel = true;
                break;
            }
        }

        private void RemoveTags_Click(object sender, RoutedEventArgs e) {
            if(lsTags.SelectedItem is ScreenshotTag tag) {
                Tags.Remove(tag);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e) {
            if(e.Key == Key.Escape) {
                Close();
                e.Handled = true;
            }
        }
    }
}
