using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MN_Sciaga.Util
{
    public class ObservableObject<T> : INotifyPropertyChanged
    {
        T _val;
        public T val
        {
            get
            {
                return _val;
            }
            set
            {
                _val = value;
                RaisePropertyChanged();
            }
        }

        void RaisePropertyChanged([CallerMemberName] string name = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public override string ToString()
        {
            return val.ToString();
        }

        public ObservableObject() { }

        public ObservableObject(T value, PropertyChangedEventHandler handler = null)
        {
            this.val = value;
            if (handler != null)
                this.PropertyChanged = handler;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
