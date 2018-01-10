using DataProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Targil4
{
    class addCommand : ICommand
    {
        public MainWindowViewModel vm { get; set; }
        public addCommand(MainWindowViewModel mainWindowViewModel)
        {
            vm = mainWindowViewModel;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }

        }



        public bool CanExecute(object parameter)
        {
            Worker worker = parameter as Worker;
            if (worker!=null&&!String.IsNullOrWhiteSpace(worker.firstName)&& !String.IsNullOrWhiteSpace(worker.lastName))
                return true;
            return false;
        }

        public void Execute(object parameter)
        {
            vm.addWorker();
        }
    }
}
