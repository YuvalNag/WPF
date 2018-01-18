using DAL;
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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for ChackWindow.xaml
    /// </summary>
    public partial class ChackWindow : Window
    {
        public ChackWindow()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dfgdf();
        }

        private async void dfgdf()
        {
            DAL_imp dAL = new DAL_imp();
            var co = await dAL.getCountries();
            var co1 = await dAL.getRTRates();
        //    var co2 = await dAL.getHRates();

            int x = 5;
        }
    }
}
