using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Reflection;

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
            UIElement element = o as UIElement;
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

        private static void HookCommand(UIElement element, ICommand command)
        {
            CommandButtonBehaviors Behaviors = new CommandButtonBehaviors(element, command);
            Behaviors.Attach();
            element.SetValue(CommandButtonBehaviorsProperty, Behaviors);
        }

        private static void UnhookCommand(UIElement element, ICommand command)
        {
            CommandButtonBehaviors Behaviors = (CommandButtonBehaviors)element.GetValue(CommandButtonBehaviorsProperty);
            Behaviors.Detach();
            element.ClearValue(CommandButtonBehaviorsProperty);
        }

        public static void SetCommand(this UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);

        }//end method

        public static ICommand GetCommand(this UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        #region Command Target

        public static readonly DependencyProperty CommandTargetProperty =
            DependencyProperty.RegisterAttached("CommandTarget", typeof(object), typeof(Behaviors),
            null);

        public static object GetCommandTarget(this DependencyObject target)
        {
            return target.GetValue(CommandTargetProperty);
        }

        public static void SetCommandTarget(this DependencyObject target, object value)
        {
            target.SetValue(CommandTargetProperty, value);
        }

        #endregion

        #region Command Trigger

        public static readonly DependencyProperty CommandTriggerProperty =
            DependencyProperty.RegisterAttached("CommandTrigger", typeof(object), typeof(Behaviors),
            null);

        public static object GetCommandTrigger(this DependencyObject target)
        {
            return target.GetValue(CommandTriggerProperty);
        }

        public static void SetCommandTrigger(this DependencyObject target, object value)
        {
            target.SetValue(CommandTriggerProperty, value);
        }

        #endregion

        #region Command Parameter

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(Behaviors),
            null);

        public static object GetCommandParameter(this UIElement element)
        {
            return element.GetValue(CommandParameterProperty);
        }

        public static void SetCommandParameter(this UIElement element, object value)
        {
            element.SetValue(CommandParameterProperty, value);
        }

        #endregion

        private class CommandButtonBehaviors
        {
            private readonly WeakReference _elementReference;
            private readonly ICommand _command;

            public CommandButtonBehaviors(UIElement element, ICommand command)
            {
                _elementReference = new WeakReference(element);
                _command = command;
            }

            public void Attach()
            {
                UIElement element = GetElement();

                System.Diagnostics.Debug.WriteLine("Attaching mouse down event: " + element);

                if (element != null)
                {
                    if (element is ButtonBase)
                    {
                        ButtonBase button = (ButtonBase)element;
                        button.Click += element_Clicked;

                        System.Diagnostics.Debug.WriteLine("Object is button: " + button);

                    }
                    else
                    {
                        element.MouseLeftButtonDown += element_Clicked;

                    }//end if
                    
                    _command.CanExecuteChanged += command_CanExecuteChanged;
                    SetIsEnabled(element);
                }
            }

            public void Detach()
            {
                _command.CanExecuteChanged -= command_CanExecuteChanged;

                UIElement element = GetElement();
                if (element != null)
                {
                    element.MouseLeftButtonDown -= element_Clicked;
                }
            }

            private void command_CanExecuteChanged(object sender, EventArgs e)
            {
                UIElement element = GetElement();
                if (element != null)
                {
                    SetIsEnabled(element);
                }
                else
                {
                    Detach();
                }
            }

            private void SetIsEnabled(UIElement element)
            {
                bool isEnabled = _command.CanExecute(element.GetValue(Behaviors.CommandParameterProperty));

                Control c = element as Control;
                if (c != null)
                    c.IsEnabled = isEnabled;

                //element.IsEnabled = command.CanExecute(element.GetValue(Behaviors.CommandParameterProperty));

            }//end method

            private static void element_Clicked(object sender, EventArgs e)
            {
                System.Diagnostics.Debug.WriteLine("element clicked: " + sender);
                DependencyObject element = (DependencyObject)sender;
                ICommand command = (ICommand)element.GetValue(CommandProperty);
                object commandParameter = element.GetValue(CommandParameterProperty);

                command.Execute(commandParameter);

            }//end method

            private static void element_Clicked(object sender, MouseButtonEventArgs e)
            {
                System.Diagnostics.Debug.WriteLine("element clicked: " + sender);
                DependencyObject element = (DependencyObject)sender;
                ICommand command = (ICommand)element.GetValue(CommandProperty);
                object commandParameter = element.GetValue(CommandParameterProperty);

                command.Execute(commandParameter);

            }//end method

            private UIElement GetElement()
            {
                return _elementReference.Target as UIElement;

            }//end method

        }//end class
    
    }//end class

}//end namespace