using PL.Commands;
using PL.UserControls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL.ViewModels
{
    public class MainVM : INotifyPropertyChanged, ISwitchUC
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
            
            selectedIndex = 0;
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
            {
                if (UC.Count == 1)
                    UC.Add(new SearchUC());
                selectedIndex = 1;
            }
            else
                selectedIndex = 0;
        }

    }
}
