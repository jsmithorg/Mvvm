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

                _view = value;

                //do transition

                LayoutRoot.Children.Clear();
                LayoutRoot.Children.Add((UIElement)_view);

            }//end set

        }//end property

        protected Grid LayoutRoot;

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
            LayoutRoot = (Grid)GetTemplateChild("LayoutRoot");

            base.OnApplyTemplate();

        }//end method

    }//end class

}//end namespace