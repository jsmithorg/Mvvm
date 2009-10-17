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
using System.Collections.Generic;

namespace JSmith.Mvvm
{
    public class MvvmLocator<T> : Dictionary<string, T> where T : ILocatable
    {
        private static MvvmLocator<T> _instance;
        public static MvvmLocator<T> Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MvvmLocator<T>();

                return _instance;

            }//end get

        }//end property

        protected MvvmLocator()
        {

        }//end constructor

    }//end class

}//end namespace