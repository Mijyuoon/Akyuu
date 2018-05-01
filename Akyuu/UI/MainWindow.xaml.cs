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
    public partial class MainWindow : Window {
        public Misc.RelayCommand SelectPageCommand { get; }

        public MainWindow() {
            InitializeComponent();

            SelectPageCommand = new Misc.RelayCommand(SelectPage_Func);
        }

        private void SelectPage_Func(object parameter) {
            var page = parameter as Models.NavigationPage;
            PageView.Source = page.Uri;
        }
    }
}
