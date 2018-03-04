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
        public IHistoryVM HistoryVM { get; set; }
        public SwitchCurrencyCommand(IHistoryVM IHistoryVM)
        {
            HistoryVM = IHistoryVM;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            HistoryVM.switchSourchCurrencyAndRelative();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


    }
}

