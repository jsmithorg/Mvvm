using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using JSmith.Mvvm.View;

namespace DragExample
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            MyDragView.DragStarted += new EventHandler(MyDragView_DragStarted);
            MyDragView.Dragging += new MouseEventHandler(MyDragView_Dragging);
            MyDragView.DragComplete += new EventHandler(MyDragView_DragComplete);
        }

        void MyDragView_DragComplete(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("DRAQ COMPLETE");
        }

        void MyDragView_Dragging(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("DRAGGING");

            DragView dv = (DragView)sender;
            dv.SetValue(Canvas.LeftProperty, e.GetPosition(null).X - (dv.Width / 2));
            dv.SetValue(Canvas.TopProperty, e.GetPosition(null).Y - (dv.Height / 2));
        }

        void MyDragView_DragStarted(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("DRAQ STARTED");
        }
    }
}
