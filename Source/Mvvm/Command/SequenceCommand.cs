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
    public class SequenceCommand : Command
    {
        public CommandCollection Children { get; internal set; }

        private int _index;
        private object _parameter;

        public SequenceCommand()
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

            _index = 0;
            _parameter = parameter;

            ExecuteCommand();

        }//end method

        private void ExecuteCommand()
        {
            try
            {
                Command c = Children[_index];
                c.Complete += new EventHandler(Command_Complete);
                c.Execute(_parameter);
            }
            catch (Exception ex)
            {
                OnFail(ex);

            }//end try

        }//end method

        private void Command_Complete(object sender, EventArgs e)
        {
            _index++;

            if (_index >= Children.Count - 1)
                OnComplete();
            else
                ExecuteCommand();

        }//end method

    }//end class

}//end namespace