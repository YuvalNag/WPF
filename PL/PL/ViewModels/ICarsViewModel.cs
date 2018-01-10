using System.Collections.ObjectModel;
using DataProtocol;

namespace PL.ViewModels
{
    public interface ICarsViewModel
    {
        ObservableCollection<Car> Cars { get; set; }
    }
}