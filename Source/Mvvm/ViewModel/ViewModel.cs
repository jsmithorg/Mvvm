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
using JSmith.Mvvm.Model;
using System.Windows.Browser;
using JSmith.Mvvm.View;

namespace JSmith.Mvvm.ViewModel
{
    public class ViewModel : Model.Model, IViewModel
    {
        protected MvvmLocator<View.IView> Views { get { return ViewLocator.Instance; } }
        protected MvvmLocator<IViewModel> ViewModels { get { return ViewModelLocator.Instance; } }

        public string Id
        {
            get { return GetValue<string>("Id"); }
            set
            {
                SetValue("Id", value);

                if (ViewModels.ContainsKey(value))
                    ViewModels[value] = this;
                else
                    ViewModels.Add(value, this);

            }//end set

        }//end property

        public ViewModel() : base() { }
        public ViewModel(string id) : this()
        {
            Id = id;

        }//end constructor

    }//end class

}//end namespace