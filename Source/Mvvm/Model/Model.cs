using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.Generic;

namespace JSmith.Mvvm.Model
{
    public class Model : IModel, INotifyPropertyChanged
    {
        private Dictionary<string, object> _values;

        #region Events

        private readonly object _lock = new object();

        private PropertyChangedEventHandler _propertyChanged;
        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                lock (_lock)
                    _propertyChanged += value;
            
            }//end add

            remove
            {
                lock (_lock)
                    _propertyChanged -= value;

            }//end remove

        }//end event

        private PropertyChangedEventHandler _propertyChanging;
        public event PropertyChangedEventHandler PropertyChanging
        {
            add
            {
                lock (_lock)
                    _propertyChanging += value;

            }//end add

            remove
            {
                lock (_lock)
                    _propertyChanging -= value;

            }//end remove

        }//end event

        #endregion

        public Model()
        {
            _values = new Dictionary<string, object>();

        }//end constructor

        protected object GetValue(string property)
        {
            if (_values.ContainsKey(property))
                return _values[property];
            else
                return null;

        }//end method

        protected T GetValue<T>(string property)
        {
            if (_values.ContainsKey(property))
                return (T)_values[property];
            else
                return default(T);

        }//end method

        protected void SetValue(string property, object value)
        {
            if (_values.ContainsKey(property))
            {
                if (_values[property] == value)
                    return;

                OnPropertyChanging(property);
                _values[property] = value;
                OnPropertyChanged(property);
            }
            else
            {
                OnPropertyChanging(property);
                _values.Add(property, value);
                OnPropertyChanged(property);

            }//end if

        }//end method

        protected void OnPropertyChanging(string property)
        {
            PropertyChangedEventHandler handler;
            lock (_lock)
            {
                handler = _propertyChanging;

            }//end lock

            if (handler != null)
                handler(this, new PropertyChangedEventArgs(property));

            //if (PropertyChanging != null)
                //PropertyChanging(this, new PropertyChangedEventArgs(property));

        }//end method

        protected void OnPropertyChanged(string property)
        {
            PropertyChangedEventHandler handler;
            lock (_lock)
            {
                handler = _propertyChanged;

            }//end lock

            if (handler != null)
                handler(this, new PropertyChangedEventArgs(property));

            //if (PropertyChanged != null)
                //PropertyChanged(this, new PropertyChangedEventArgs(property));

        }//end method

    }//end class

}//end namespace