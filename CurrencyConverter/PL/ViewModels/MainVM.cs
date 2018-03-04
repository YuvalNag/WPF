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
    public class MainVM : INotifyPropertyChanged, IMainVM
    {
        private ObservableCollection<UserControl> _UC;
        public ObservableCollection<UserControl> UC
        {
            get
            {
                return _UC;
            }
            set
            {
                _UC = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UC"));
            }
        }
        public MainVM()
        {
            UC = new ObservableCollection<UserControl>();
            UC.Add(new HistoryUC());
            UC.Add(new CountriesListUC());
            selectedIndex = 1;
            switchCommand = new SwitchUCommand(this);
        }
        public event PropertyChangedEventHandler PropertyChanged;


        private int _selectedIndex;
        public int selectedIndex
        {
            set
            {
                _selectedIndex = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("selectedIndex"));
            }
            get
            {
                return _selectedIndex;
            }
        }
        //private UserControl _selectedItem;
        //public UserControl selectedItem
        //{
        //    set
        //    {
        //        _selectedItem = value;

        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("selectedItem"));
        //    }
        //    get
        //    {
        //        return _selectedItem;
        //    }
        //}
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
            if (selectedIndex == 0)
                selectedIndex = 1;
            else
                selectedIndex = 0;
        }

    }
}
