﻿using PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL.UserControls
{
    /// <summary>
    /// Interaction logic for CountriesListUC.xaml
    /// </summary>
    public partial class CurrenciesListUC : UserControl
    {

        private CurrenciesListVM currenciesListVM { get; set; }

        public string filterString
        {
            get { return (string)GetValue(filterStringProperty); }
            set { SetValue(filterStringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for filterString.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty filterStringProperty =
            DependencyProperty.Register("filterString", typeof(string), typeof(CurrenciesListUC), new FrameworkPropertyMetadata(){DefaultValue=null, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, BindsTwoWayByDefault = true ,PropertyChangedCallback=filterStringChangedCallback});

        private static void filterStringChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
           ((CurrenciesListUC)d).currenciesListVM.FilterString = e.NewValue.ToString();
        }

        

        public CurrenciesListUC()
        {
            InitializeComponent();
            currenciesListVM = new CurrenciesListVM();
            this.DataContext = currenciesListVM;
        }
    }
}
