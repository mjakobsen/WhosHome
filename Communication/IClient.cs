using System.Collections.ObjectModel;
using System.ServiceModel;
using WhosHome.Logic;

namespace WhosHome.Communication
{
    public interface IClient
    {
        [OperationContract]
        void UpdateList();

        [OperationContract]
        void HandleAction(ObservableCollection<Vehicle> newList);
    }
}