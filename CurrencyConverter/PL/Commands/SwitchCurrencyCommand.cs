using PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PL.Commands
{
   public class SwitchCurrencyCommand : ICommand
    {
        public IHistoryVM VM { get; set; }
        


        public SwitchCurrencyCommand(IHistoryVM IVM)
        {
            VM = IVM;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            
                VM.SwitchSourceCurrencyAndRelative();
            //if (VM is ICurrenciesListVM)
            //    (VM as ICurrenciesListVM).SwitchRTListAndRTChart();


        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


    }
}

