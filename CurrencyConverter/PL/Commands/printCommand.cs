using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PL.Commands
{
    class PrintCommand : ICommand
    {
        

        public bool CanExecute(object parameter)
        {
            return parameter as SfChart != null;
        }

        public void Execute(object parameter)
        {
            ((SfChart)parameter).Print();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
