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
using JSmith.Mvvm.View;
using JSmith.Mvvm.ViewModel;

namespace JSmith.Mvvm.Command
{
    public abstract class Command : ICommand
    {
        #region Events

        private readonly object _lock = new object();

        private EventHandler _begin;
        public event EventHandler Begin
        {
            add
            {
                lock (_lock)
                    _begin += value;

            }//end add

            remove
            {
                lock (_lock)
                    _begin -= value;

            }//end remove

        }//end event

        private EventHandler _complete;
        public event EventHandler Complete
        {
            add
            {
                lock (_lock)
                    _complete += value;

            }//end add

            remove
            {
                lock (_lock)
                    _complete -= value;

            }//end remove

        }//end event

        private EventHandler _fail;
        public event EventHandler Fail
        {
            add
            {
                lock (_lock)
                    _fail += value;

            }//end add

            remove
            {
                lock (_lock)
                    _fail -= value;

            }//end remove

        }//end event

        #endregion

        #region Fields / Properties

        protected MvvmLocator<IView> Views { get { return ViewLocator.Instance; } }
        protected MvvmLocator<ViewModel.IViewModel> ViewModels { get { return ViewModelLocator.Instance; } }

        #endregion

        #region ICommand Members

        private EventHandler _canExecuteChanged;
        public event EventHandler CanExecuteChanged
        {
            add
            {
                lock (_lock)
                    _canExecuteChanged += value;

            }//end add

            remove
            {
                lock (_lock)
                    _canExecuteChanged -= value;

            }//end remove

        }//end event

        public abstract bool CanExecute(object parameter);

        public abstract void Execute(object parameter);

        #endregion

        protected virtual void OnCanExecuteChanged()
        {
            EventHandler handler;
            lock (_lock)
            {
                handler = _canExecuteChanged;

            }//end lock

            if (handler != null)
                handler(this, new EventArgs());

        }//end method

        protected virtual void OnBegin()
        {
            EventHandler handler;
            lock (_lock)
            {
                handler = _begin;

            }//end lock

            if (handler != null)
                handler(this, new EventArgs());

        }//end method

        protected virtual void OnComplete()
        {
            EventHandler handler;
            lock (_lock)
            {
                handler = _complete;

            }//end lock

            if (handler != null)
                handler(this, new EventArgs());

        }//end method

        protected virtual void OnFail(Exception ex)
        {
            EventHandler handler;
            lock (_lock)
            {
                handler = _fail;

            }//end lock

            if (handler != null)
                handler(this, new EventArgs());

        }//end method

    }//end class

}//end namespace