using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PL.ViewModels
{
    public class MainVM : INotifyPropertyChanged, IMainVM
    {
        public MainVM()
        {
            isRTView = true;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isRTView;
        public bool isRTView
        {
            set
            {
                _isRTView = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("isRTView"));
            }
            get
            {
                return _isRTView;
            }
        }
        private ICommand _switchCommand;
        public ICommand switchCommand
        {
            get
            {
                return _switchCommand;
            }
            set
            {
                _switchCommand = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("switchCommand"));
            }
        }
        public void SwitchUCSelected()
        {
            isRTView = false;
        }

    }
}
