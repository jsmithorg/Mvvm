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

namespace JSmith.Mvvm.View
{
    public class DraggableUserControl : UserControl, IDraggable
    {
        public DraggableUserControl()
        {
            MouseLeftButtonDown += new MouseButtonEventHandler(ViewBase_MouseLeftButtonDown);

        }//end method

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

        public bool IsDragging { get; internal set; }

        public void BeginDrag()
        {
            IsDragging = true;

            //on drag started
            OnDragStarted();

            MouseMove += new MouseEventHandler(ViewBase_MouseMove);
            MouseLeave += new MouseEventHandler(ViewBase_MouseLeave);

        }//end method

        public void EndDrag()
        {
            MouseMove -= new MouseEventHandler(ViewBase_MouseMove);
            MouseLeave -= new MouseEventHandler(ViewBase_MouseLeave);

            IsDragging = false;

            //on drag complete
            OnDragComplete();

        }//end method

        #endregion

        private void ViewBase_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ViewBase vb = (ViewBase)sender;
            MouseLeftButtonUp += new MouseButtonEventHandler(ViewBase_MouseLeftButtonUp);
            vb.MouseMove += new MouseEventHandler(ViewBase_FirstMouseMove);

        }//end method

        private void ViewBase_FirstMouseMove(object sender, MouseEventArgs e)
        {
            ViewBase vb = (ViewBase)sender;
            vb.MouseMove -= new MouseEventHandler(ViewBase_FirstMouseMove);

            BeginDrag();

        }//end method

        private void ViewBase_MouseMove(object sender, MouseEventArgs e)
        {
            //on dragging
            OnDragging(e);

        }//end method

        private void ViewBase_MouseLeave(object sender, MouseEventArgs e)
        {
            if (IsDragging)
                EndDrag();

        }//end method

        private void ViewBase_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ViewBase vb = (ViewBase)sender;
            vb.MouseLeftButtonUp -= new MouseButtonEventHandler(ViewBase_MouseLeftButtonUp);
            vb.MouseMove -= new MouseEventHandler(ViewBase_FirstMouseMove);

            if (IsDragging)
                EndDrag();

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

    }//end class

}//end namespace