using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace JSmith.Mvvm
{
    public static partial class Behaviors
    {
        private static readonly DependencyProperty CommandButtonBehaviorsProperty =
            DependencyProperty.RegisterAttached("CommandButtonBehaviors", typeof(CommandButtonBehaviors),
                                                typeof(Behaviors), null);

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(Behaviors),
            new PropertyMetadata(CommandPropertyChanged));

        private static void CommandPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ButtonBase element = o as ButtonBase;
            if (element != null)
            {
                if (e.OldValue != null)
                {
                    UnhookCommand(element, (ICommand)e.OldValue);
                }
                if (e.NewValue != null)
                {
                    HookCommand(element, (ICommand)e.NewValue);
                }
            }
        }

        private static void HookCommand(ButtonBase element, ICommand command)
        {
            CommandButtonBehaviors Behaviors = new CommandButtonBehaviors(element, command);
            Behaviors.Attach();
            element.SetValue(CommandButtonBehaviorsProperty, Behaviors);
        }

        private static void UnhookCommand(ButtonBase element, ICommand command)
        {
            CommandButtonBehaviors Behaviors = (CommandButtonBehaviors)element.GetValue(CommandButtonBehaviorsProperty);
            Behaviors.Detach();
            element.ClearValue(CommandButtonBehaviorsProperty);
        }

        public static void SetCommand(this Button b, ICommand command)
        {
            b.SetValue(Behaviors.CommandProperty, command);

        }//end method

        public static ICommand GetCommand(ButtonBase buttonBase)
        {
            return (ICommand)buttonBase.GetValue(CommandProperty);
        }

        public static void SetCommand(ButtonBase buttonBase, ICommand value)
        {
            buttonBase.SetValue(CommandProperty, value);
        }

        #region Command Target

        public static readonly DependencyProperty CommandTargetProperty =
            DependencyProperty.RegisterAttached("CommandTarget", typeof(object), typeof(Behaviors),
            null);

        public static object GetCommandTarget(DependencyObject target)
        {
            return target.GetValue(CommandTargetProperty);
        }

        public static void SetCommandTarget(DependencyObject target, object value)
        {
            target.SetValue(CommandTargetProperty, value);
        }

        #endregion

        #region Command Parameter

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(Behaviors),
            null);

        public static object GetCommandParameter(ButtonBase buttonBase)
        {
            return buttonBase.GetValue(CommandParameterProperty);
        }

        public static void SetCommandParameter(ButtonBase buttonBase, object value)
        {
            buttonBase.SetValue(CommandParameterProperty, value);
        }

        #endregion

        private class CommandButtonBehaviors
        {
            private readonly WeakReference elementReference;
            private readonly ICommand command;

            public CommandButtonBehaviors(ButtonBase element, ICommand command)
            {
                this.elementReference = new WeakReference(element);
                this.command = command;
            }

            public void Attach()
            {
                ButtonBase element = GetElement();
                if (element != null)
                {
                    element.Click += element_Clicked;
                    command.CanExecuteChanged += command_CanExecuteChanged;
                    SetIsEnabled(element);
                }
            }

            public void Detach()
            {
                command.CanExecuteChanged -= command_CanExecuteChanged;
                ButtonBase element = GetElement();
                if (element != null)
                {
                    element.Click -= element_Clicked;
                }
            }

            void command_CanExecuteChanged(object sender, EventArgs e)
            {
                ButtonBase element = GetElement();
                if (element != null)
                {
                    SetIsEnabled(element);
                }
                else
                {
                    Detach();
                }
            }

            private void SetIsEnabled(ButtonBase element)
            {
                element.IsEnabled = command.CanExecute(element.GetValue(Behaviors.CommandParameterProperty));
            }

            private static void element_Clicked(object sender, EventArgs e)
            {
                DependencyObject element = (DependencyObject)sender;
                ICommand command = (ICommand)element.GetValue(CommandProperty);
                object commandParameter = element.GetValue(CommandParameterProperty);

                //MVCEventArgs args = new MVCEventArgs();
                //args.Sender = element;
                //args.Data = commandParameter;
                command.Execute(commandParameter);
            }

            private ButtonBase GetElement()
            {
                return elementReference.Target as ButtonBase;
            }
        }
    }
}
