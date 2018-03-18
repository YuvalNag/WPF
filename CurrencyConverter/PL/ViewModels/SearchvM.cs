using PL.Commands;
using PL.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL.ViewModels 
{
    class SearchVM: INotifyPropertyChanged, ISwitchUC
    {
        public SearchVM()
        {
            
            selectedUC = new CurrenciesListUC();
            switchCommand = new SwitchUCommand(this);
          
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

        private UserControl _selectedUC;

        public event PropertyChangedEventHandler PropertyChanged;

        public UserControl selectedUC
        {
            get
            {
                return _selectedUC;
            }
            set
            {
                _selectedUC = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("selectedUC"));
            }
        }

        private UserControl _prevUC { set; get; }
  

        public void SwitchUCSelected()
        {
            if (_prevUC ==null)
            {
                _prevUC = new RTChartUS();
            }

            UserControl temp = _prevUC;
            _prevUC = selectedUC;
            selectedUC = temp;
        }
    }
}
