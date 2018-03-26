using Microsoft.Win32;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace PL.Commands
{
    class SaveCommand : ICommand
    {
        

        public bool CanExecute(object parameter)
        {
            return parameter as SfChart != null;
        }

        public void Execute(object parameter)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "Untitled";
            sfd.Filter = "Bitmap(*.bmp)|*.bmp|JPEG(*.jpg,*.jpeg)|*.jpg;*.jpeg|Gif (*.gif)|*.gif|PNG(*.png)|*.png|All files (*.*)|*.*";
            if (sfd.ShowDialog() == true)
            {
                using (Stream fs = sfd.OpenFile())
                {
                    ((SfChart)parameter).Save(fs, new JpegBitmapEncoder());
                }
            }
            
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
