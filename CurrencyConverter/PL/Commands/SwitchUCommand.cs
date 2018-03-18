using PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PL.Commands
{
    class SwitchUCommand : ICommand
    {
        public ISwitchUC switchUC { get; set; }
        public SwitchUCommand(ISwitchUC switchUC)
        {
            this.switchUC = switchUC;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            switchUC.SwitchUCSelected();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

    }
}
