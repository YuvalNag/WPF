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
using Microsoft.Win32;
using System.IO;

namespace PL.UserControls
{
    /// <summary>
    /// Interaction logic for HistoryUC.xaml
    /// </summary>
    public partial class HistoryUC : UserControl
    {
        public HistoryUC()
        {
            InitializeComponent();
            series.StrokeThickness = 0.3;
            series.Stroke = new SolidColorBrush(Color.FromArgb(255, 27, 161, 226));

            


            SeriesInRangenavigator.StrokeThickness = 0.3;
            SeriesInRangenavigator.Stroke = new SolidColorBrush(Color.FromArgb(255, 27, 161, 226));

        }

     
    }
}
