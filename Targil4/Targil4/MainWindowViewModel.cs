using DataProtocol;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Targil4
{
    class MainWindowViewModel:INotifyPropertyChanged
    {
        private ObservableCollection<Worker> workers_ObservableCollection; 
        public ICollectionView workers { set; get; }


        private Worker worker;
        public Worker currentWorker
        {
            get { return worker; }
            set
            {
                worker = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("currentWorker"));
            }


        }

        private string _filterString;
        public string FilterString
        {
            get { return _filterString; }
            set
            {
                _filterString = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FilterString"));
                workers.Refresh();
            }
        }

     
     
        public addCommand addCommand { get; set; }
        private Model model;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            model = new Model();
            addCommand = new addCommand(this);
            workers_ObservableCollection = new ObservableCollection<Worker>(model.workerList);
            workers = CollectionViewSource.GetDefaultView(workers_ObservableCollection);
            currentWorker =   new Worker();
            workers.Filter = WorkerFilter;
            workers.CollectionChanged += Workers_CollectionChanged;
           
        }



        public void addWorker()
        {
            workers_ObservableCollection.Add(worker);
            currentWorker = new Worker();
        }

        private void Workers_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            
            if(e.Action == NotifyCollectionChangedAction.Add && e.NewItems != null)
                 model.addWorker(e.NewItems[0] as Worker);

        }


        private bool WorkerFilter(object item)
        {
            Worker worker = item as Worker;
            if (String.IsNullOrWhiteSpace(_filterString) || worker == null || String.IsNullOrWhiteSpace(worker.lastName))
                return true;
            return worker.lastName.Contains(_filterString);
        }
    }
}
