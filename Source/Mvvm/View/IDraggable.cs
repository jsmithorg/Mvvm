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
    public interface IDraggable
    {
        event EventHandler DragStarted;
        event MouseEventHandler Dragging;
        event EventHandler DragComplete;

        void BeginDrag();
        void EndDrag();

        bool IsDragging { get; }

    }//end interface

}//end namespace