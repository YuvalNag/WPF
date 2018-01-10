using DataProtocol;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil4
{
    class MainWindowViewModel:INotifyPropertyChanged
    {
        public ObservableCollection<Worker> workers { set; get; }
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
        public addCommand addCommand { get; set; }
        private Model model;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            model = new Model();
            addCommand = new addCommand(this);
            workers = new ObservableCollection<Worker>(model.workerList);
            currentWorker=   new Worker();
           
            workers.CollectionChanged += Workers_CollectionChanged;
           
        }

        public void addWorker()
        {
            workers.Add(worker);
            currentWorker = new Worker();
        }

        private void Workers_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            
            if(e.Action == NotifyCollectionChangedAction.Add && e.NewItems != null)
                 model.addWorker(e.NewItems[0] as Worker);

        }


    }
}
