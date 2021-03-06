﻿using System;
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
    public class View : ViewBase, IView
    {
        private bool _contentLoaded;

        #region Constructor

        public View() : base()
        {
            InitializeComponent();

        }//end constructor

        public View(string id) : base(id)
        {
            InitializeComponent();

        }//end constructor

        #endregion

        private void InitializeComponent()
        {
            if (_contentLoaded)
                return;

            _contentLoaded = true;

            Type t = GetType();
            string assemblyName = t.AssemblyQualifiedName.Split(',')[1].Trim();
            string xamlPath = t.FullName.Substring(assemblyName.Length).Replace(".", "/");
            string componentPath = "/" + assemblyName + ";component" + xamlPath + ".xaml";

            System.Windows.Application.LoadComponent(this, new System.Uri(componentPath, System.UriKind.Relative));
            //System.Windows.Application.LoadComponent(this, new System.Uri("/Microsoft.ProPhoto.Marquee;component/Views/MainView.xaml", System.UriKind.Relative));
            
        }//end method

    }//end class

}//end namespace