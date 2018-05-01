using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akyuu.Models {
    public class NavigationPage : BaseModel {
        private string _Title;
        public string Title {
            get { return _Title; }
            set { SetProperty(ref _Title, value, nameof(Title)); }
        }

        private Uri _Uri;
        public Uri Uri {
            get { return _Uri; }
            set { SetProperty(ref _Uri, value, nameof(Uri)); }
        }
    }

    public class NavigationPageList : ObservableCollection<NavigationPage> { }
}
