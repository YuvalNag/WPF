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
    class SearchVM: BaseVM, ISwitchUC
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
                OnPropertyChanged();
            }
        }

        private UserControl _selectedUC;
        public UserControl selectedUC
        {
            get
            {
                return _selectedUC;
            }
            set
            {
                _selectedUC = value;
                OnPropertyChanged();
            }
        }

        private UserControl _unSelectedUC { set; get; }
  

        public void SwitchUCSelected()
        {
            if (_unSelectedUC ==null)
            {
                _unSelectedUC = new RTChartUS();
            }

            UserControl temp = _unSelectedUC;
            _unSelectedUC = selectedUC;
            selectedUC = temp;
        }
    }
}
