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
using JSmith.Mvvm.View;
using System.Windows.Markup;

namespace JSmith.Mvvm.View
{
    public class ViewController : Control
    {
        private IView _view;
        public IView View
        {
            get { return _view; }
            set
            {
                if (_view == value)
                    return;

                lastView = _view;
                _view = value;

                OnViewAdded();

            }//end set

        }//end property

        protected IView lastView;
        protected Grid layoutRoot;

        public ViewController()
        {
            string controlTemplate = @"
                <ControlTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
                                 xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"" TargetType=""Control"">
                    <Grid x:Name=""LayoutRoot"" />
                </ControlTemplate>";

            Template = (ControlTemplate)XamlReader.Load(controlTemplate);

            ApplyTemplate();

        }//end constructor

        public override void OnApplyTemplate()
        {
            layoutRoot = (Grid)GetTemplateChild("LayoutRoot");

            base.OnApplyTemplate();

        }//end method

        protected virtual void OnViewAdded()
        {
            layoutRoot.Children.Clear();
            layoutRoot.Children.Add((UIElement)_view);

        }//end method

    }//end class

}//end namespace