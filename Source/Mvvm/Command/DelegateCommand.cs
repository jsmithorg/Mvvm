using System;

namespace JSmith.Mvvm.Command
{
    /// <summary>
    /// A command that executes delegates to determine whether the command can execute, and to execute the command.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This command implementation is useful when the command simply needs to execute a method on a view model. The delegate for
    /// determining whether the command can execute is optional. If it is not provided, the command is considered always eligible
    /// to execute.
    /// </para>
    /// </remarks>
    public class DelegateCommand<T> : Command
    {
        private readonly Func<T, bool> _canExecute;
        private readonly Action<T> _execute;

        /// <summary>
        /// Constructs an instance of <c>DelegateCommand</c>.
        /// </summary>
        /// <remarks>
        /// This constructor creates the command without a delegate for determining whether the command can execute. Therefore, the
        /// command will always be eligible for execution.
        /// </remarks>
        /// <param name="execute">
        /// The delegate to invoke when the command is executed.
        /// </param>
        public DelegateCommand(Action<T> execute) : this(execute, null) { }

        /// <summary>
        /// Constructs an instance of <c>DelegateCommand</c>.
        /// </summary>
        /// <param name="execute">
        /// The delegate to invoke when the command is executed.
        /// </param>
        /// <param name="canExecute">
        /// The delegate to invoke to determine whether the command can execute.
        /// </param>
        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            //execute.AssertNotNull("execute");
            _execute = execute;
            _canExecute = canExecute;

        }//end method

        /// <summary>
        /// Determines whether this command can execute.
        /// </summary>
        /// <remarks>
        /// If there is no delegate to determine whether the command can execute, this method will return <see langword="true"/>. If a delegate was provided, this
        /// method will invoke that delegate.
        /// </remarks>
        /// <param name="parameter">
        /// The command parameter.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the command can execute, otherwise <see langword="false"/>.
        /// </returns>
        public override bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;

            return _canExecute((T)parameter);

        }//end method

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <remarks>
        /// This method invokes the provided delegate to execute the command.
        /// </remarks>
        /// <param name="parameter">
        /// The command parameter.
        /// </param>
        public override void Execute(object parameter)
        {
            OnBegin();

            try
            {
                _execute((T)parameter);
            }
            catch (Exception ex)
            {
                OnFail(ex);

                return;

            }//end try

            OnComplete();

        }//end method

    }//end class

}//end namespace