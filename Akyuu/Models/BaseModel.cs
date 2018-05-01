using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Akyuu.Models {
    [DataContract]
    public class BaseModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetProperty<T>(ref T oldValue, T newValue, string propName) {
            var comp = EqualityComparer<T>.Default;
            if(comp.Equals(oldValue, newValue)) return;

            oldValue = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        protected void SetProperty<T>(ref T oldValue, T newValue, params string[] propNames) {
            var comp = EqualityComparer<T>.Default;
            if(comp.Equals(oldValue, newValue)) return;

            oldValue = newValue;
            if(PropertyChanged == null) return;

            foreach(var propName in propNames) {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
