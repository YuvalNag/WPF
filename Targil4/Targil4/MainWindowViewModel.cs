using DataProtocol;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil4
{
    class MainWindowViewModel
    {
        public ObservableCollection<Worker> workers { set; get; }
        //public Worker worker { get; set; }
        public addCommand addCommand { get; set; }
        private Model model;

        public MainWindowViewModel()
        {
            model = new Model();
            addCommand = new addCommand(this);
            workers = new ObservableCollection<Worker>();
            workers.CollectionChanged += Workers_CollectionChanged;
            for (int i = 0; i < 10; i++)
            {
                workers.Add(new Worker() { id = i, firstName = "first name " + i, lastName = "last name " + i, educuation = (EDUCUATION)(i % 5) });
            }
        }

        private void Workers_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            
            if(e.Action == NotifyCollectionChangedAction.Add && e.NewItems != null)
                 model.addWorker(e.NewItems[0] as Worker);

        }


    }
}
