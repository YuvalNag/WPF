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
    /// Interaction logic for ConverterUC.xaml
    /// </summary>
    public partial class ConverterUC : UserControl
    {
        public ConverterUC()
        {
            InitializeComponent();
            AmountTextBox.DataContext = Amount;
        }


        public string Amount
        {
            get { return (string)GetValue(AmountProperty); }
            set { SetValue(AmountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Amount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AmountProperty =
            DependencyProperty.Register("Amount", typeof(string), typeof(ConverterUC), new FrameworkPropertyMetadata("1", FrameworkPropertyMetadataOptions.AffectsRender,
                                                new PropertyChangedCallback(AmountStateChangedCallBack),
                                                new CoerceValueCallback(AmountFixValueCallBack))

                );

        private static object AmountFixValueCallBack(DependencyObject d, object baseValue)
        {
            return baseValue;
        }

        private static void AmountStateChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ConverterUC temp = d as ConverterUC;

            if (temp != null)
            {
                temp.AmountTextBox.Text = e.NewValue.ToString();
            }
        }
    }
}
