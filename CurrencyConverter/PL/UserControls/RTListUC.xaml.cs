using DP;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for RTListUC.xaml
    /// </summary>
    public partial class RTListUC : UserControl
    {



        public ObservableCollection<Currency> itemSource
        {
            get { return (ObservableCollection<Currency>)GetValue(itemSourceProperty); }
            set { SetValue(itemSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty itemSourceProperty =
            DependencyProperty.Register("itemSource", typeof(ObservableCollection<Currency>), typeof(RTListUC), new FrameworkPropertyMetadata {DefaultValue=new ObservableCollection<Currency>(),DefaultUpdateSourceTrigger=UpdateSourceTrigger.PropertyChanged,BindsTwoWayByDefault=true},validateValueCallback);

        private static bool validateValueCallback(object value)
        {
            int x = 5;
            return true;
        }

       

        public int amount
        {
            get { return (int)GetValue(amountProperty); }
            set { SetValue(amountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for amount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty amountProperty =
            DependencyProperty.Register("amount", typeof(int), typeof(RTListUC), new PropertyMetadata(0));



        public RTListUC()
        {
            InitializeComponent();
            list.DataContext = this;
        }
    }
}
