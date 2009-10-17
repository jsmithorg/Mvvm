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

    }//end class

}//end namespace