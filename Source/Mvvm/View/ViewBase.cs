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
using JSmith.Mvvm.ViewModel;
using System.Windows.Browser;

namespace JSmith.Mvvm.View
{
    public class ViewBase : UserControl, IView, IDraggable
    {
        public const string DefaultRootElementName = "LayoutRoot";

        #region Fields / Properties

        protected MvvmLocator<IView> Views { get { return ViewLocator.Instance; } }
        protected MvvmLocator<IViewModel> ViewModels { get { return ViewModelLocator.Instance; } }

        #region IView Members

        public virtual IViewModel ViewModel
        {
            get { return (IViewModel)DataContext; }
            set { DataContext = value; }

        }//end property

        #endregion

        #region ILocatable Members

        private string _id;
        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;

                if (Views.ContainsKey(value))
                    Views[value] = this;
                else
                    Views.Add(value, this);

            }//end set

        }//end property

        #endregion

        #endregion

        #region Constructor

        public ViewBase()
        {
            //MouseLeftButtonDown += new MouseButtonEventHandler(ViewBase_MouseLeftButtonDown);

        }//end constructor

        public ViewBase(string id) : this()
        {
            Id = id;

        }//end constructor

        #endregion

        #region IDraggable Members

        private readonly object _lock = new object();

        private EventHandler _dragStarted;
        public event EventHandler DragStarted
        {
            add
            {
                lock (_lock)
                    _dragStarted += value;

            }//end add

            remove
            {
                lock (_lock)
                    _dragStarted -= value;

            }//end remove

        }//end event

        private MouseEventHandler _dragging;
        public event MouseEventHandler Dragging
        {
            add
            {
                lock (_lock)
                    _dragging += value;

            }//end add

            remove
            {
                lock (_lock)
                    _dragging -= value;

            }//end remove

        }//end event

        private EventHandler _dragComplete;
        public event EventHandler DragComplete
        {
            add
            {
                lock (_lock)
                    _dragComplete += value;

            }//end add

            remove
            {
                lock (_lock)
                    _dragComplete -= value;

            }//end remove

        }//end event

        public void BeginDrag()
        {
            //on drag started
            OnDragStarted();

            MouseMove += new MouseEventHandler(ViewBase_MouseMove);

        }//end method

        public void EndDrag()
        {
            MouseMove -= new MouseEventHandler(ViewBase_MouseMove);

            //on drag complete
            OnDragComplete();

        }//end method

        #endregion

        private void ViewBase_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //on drag started
            //OnDragStarted();

            //ViewBase vb = (ViewBase)sender;
            //vb.MouseMove += new MouseEventHandler(ViewBase_MouseMove);
            //vb.MouseLeftButtonUp += new MouseButtonEventHandler(ViewBase_MouseLeftButtonUp);

        }//end method

        private void ViewBase_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //ViewBase vb = (ViewBase)sender;
            //vb.MouseMove -= new MouseEventHandler(ViewBase_MouseMove);
            //vb.MouseLeftButtonUp -= new MouseButtonEventHandler(ViewBase_MouseLeftButtonUp);

            //on drag complete
            //OnDragComplete();

        }//end method

        private void ViewBase_MouseMove(object sender, MouseEventArgs e)
        {
            //on dragging
            OnDragging(e);

        }//end method

        protected virtual void OnDragStarted()
        {
            EventHandler handler;
            lock (_lock)
            {
                handler = _dragStarted;

            }//end lock

            if (handler != null)
                handler(this, new EventArgs());
            
        }//end method

        protected virtual void OnDragging(MouseEventArgs e)
        {
            MouseEventHandler handler;
            lock (_lock)
            {
                handler = _dragging;

            }//end lock

            if (handler != null)
                handler(this, e);

        }//end method

        protected virtual void OnDragComplete()
        {
            EventHandler handler;
            lock (_lock)
            {
                handler = _dragComplete;

            }//end lock

            if (handler != null)
                handler(this, new EventArgs());

        }//end method

        #region Methods

        protected object GetRootElement()
        {
            return FindName(DefaultRootElementName);

        }//end method

        protected T GetRootElement<T>()
        {
            return (T)FindName(DefaultRootElementName);

        }//end method

        protected object GetRootElement(string rootElementName)
        {
            return FindName(rootElementName);

        }//end method

        protected T GetRootElement<T>(string rootElementName)
        {
            return (T)FindName(rootElementName);

        }//end method

        protected object GetElement(string elementName)
        {
            return FindName(elementName);

        }//end method

        protected T GetElement<T>(string elementName)
        {
            return (T)FindName(elementName);

        }//end method

        #endregion

    }//end class

}//end namespace