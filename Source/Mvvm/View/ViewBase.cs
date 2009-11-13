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
using JSmith.Mvvm.ViewModel;
using System.Windows.Browser;

namespace JSmith.Mvvm.View
{
    public class ViewBase : UserControl, IView
    {
        public const string DefaultRootElementName = "LayoutRoot";

        #region Fields / Properties

        protected MvvmLocator<IView> Views { get { return ViewLocator.Instance; } }
        protected MvvmLocator<ViewModel.IViewModel> ViewModels { get { return ViewModelLocator.Instance; } }

        #endregion

        #region ILocatable Members

        private string _id;
        public string ID
        {
            get { return _id; }
            set
            {
                _id = value;

                if (Views.ContainsKey(value))
                    Views[value] = this;
                else
                    Views.Add(value, this);
                
            }//end set

        }//end property

        #endregion

        #region Constructor

        public ViewBase() { }

        public ViewBase(string id) : this()
        {
            ID = id;

        }//end constructor

        #endregion

        protected object GetRootElement()
        {
            return FindName(DefaultRootElementName);

        }//end method

        protected T GetRootElement<T>()
        {
            return (T)FindName(DefaultRootElementName);

        }//end method

        protected object GetRootElement(string rootElementName)
        {
            return FindName(rootElementName);

        }//end method

        protected T GetRootElement<T>(string rootElementName)
        {
            return (T)FindName(rootElementName);

        }//end method

        protected object GetElement(string elementName)
        {
            return FindName(elementName);

        }//end method

        protected T GetElement<T>(string elementName)
        {
            return (T)FindName(elementName);

        }//end method

    }//end class

}//end namespace