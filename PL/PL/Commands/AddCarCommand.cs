using DataProtocol;
using PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PL.Commands
{
    public class AddCarCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        private ICarsViewModel CurrentVM;

        //גישה ראשונה להגדרת יחסים בין הפקודה לויוי מודל 
        // using DI
        public AddCarCommand(ICarsViewModel Vm)
        {
            CurrentVM = Vm;
        }

        // גישה שניה תשתמש בהשמת אירוע
        public event Action<string> CarAdded;
        public AddCarCommand()
        {
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            //CurrentVM.Cars.Add(new Car { Id = -1, Description = "mazda" });
            //OR - but fix observable update ...
            if (CarAdded != null)
                CarAdded("mazda");
        }
    }
}
