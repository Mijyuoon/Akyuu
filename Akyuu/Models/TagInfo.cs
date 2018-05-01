using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akyuu.Models {
    public class TagInfo : BaseModel {
        private string _Name;
        public string Name {
            get { return _Name; }
            set { SetProperty(ref _Name, value, nameof(Name)); }
        }

        private int _Count;
        public int Count {
            get { return _Count; }
            set { SetProperty(ref _Count, value, nameof(Count)); }
        }

        private bool _Selected;
        public bool Selected {
            get { return _Selected; }
            set { SetProperty(ref _Selected, value, nameof(Selected)); }
        }
    }
}
