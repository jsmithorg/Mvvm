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
    public class CompositeCommand : Command
    {
        public CommandCollection Children { get; internal set; }

        private int _completeCount;

        public CompositeCommand()
        {
            Children = new CommandCollection();

        }//end constructor

        public override bool CanExecute(object parameter)
        {
            for (int i = 0; i < Children.Count; i++)
                if (!Children[i].CanExecute(parameter))
                    return false;

            return true;

        }//end method

        public override void Execute(object parameter)
        {
            OnBegin();

            for (int i = 0; i < Children.Count; i++)
            {
                try
                {
                    Command c = Children[i];
                    c.Complete += new EventHandler(Command_Complete);
                    c.Execute(parameter);
                }
                catch (Exception ex)
                {
                    OnFail(ex);

                }//end try

            }//end for

        }//end method

        private void Command_Complete(object sender, EventArgs e)
        {
            _completeCount++;

            if (_completeCount >= Children.Count - 1)
                OnComplete();

        }//end method

    }//end class

}//end namespace