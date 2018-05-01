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
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Akyuu.UI.Components {
    public partial class FilePickerBox : UserControl {
        public FilePickerBox() {
            InitializeComponent();
        }

        public string FilePath {
            get { return (string)GetValue(FilePathProperty); }
            set { SetValue(FilePathProperty, value); }
        }

        public static readonly DependencyProperty FilePathProperty =
            DependencyProperty.Register("FilePath", typeof(string), typeof(FilePickerBox),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string InitialDirectory {
            get { return (string)GetValue(InitialDirectoryProperty); }
            set { SetValue(InitialDirectoryProperty, value); }
        }
        
        public static readonly DependencyProperty InitialDirectoryProperty =
            DependencyProperty.Register("InitialDirectory", typeof(string), typeof(FilePickerBox), new PropertyMetadata(null));

        public bool PickFolders {
            get { return (bool)GetValue(PickFoldersProperty); }
            set { SetValue(PickFoldersProperty, value); }
        }
        
        public static readonly DependencyProperty PickFoldersProperty =
            DependencyProperty.Register("PickFolders", typeof(bool), typeof(FilePickerBox), new PropertyMetadata(false));

        private void Browse_Click(object sender, RoutedEventArgs e) {
            if(PickFolders) {
                var dialog = new CommonOpenFileDialog {
                    IsFolderPicker = true,
                    EnsureFileExists = true,
                    Multiselect = false,

                    InitialDirectory = InitialDirectory ?? Environment.CurrentDirectory,
                };

                if(dialog.ShowDialog() == CommonFileDialogResult.Ok) {
                    FilePath = dialog.FileName;
                }
            } else {
                throw new NotImplementedException();
            }
        }
    }
}
