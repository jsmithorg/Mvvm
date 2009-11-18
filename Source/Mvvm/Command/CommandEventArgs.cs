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

namespace JSmith.Mvvm.Command
{
    public class CommandEventArgs : EventArgs
    {
        public object Sender { get; internal set; }
        public object Parameter { get; internal set; }

        public CommandEventArgs() : base() { }

        public CommandEventArgs(object sender, object parameter) : this()
        {
            Sender = sender;
            Parameter = parameter;

        }//end constructor

    }//end class

}//end namespace