using System.Collections.ObjectModel;
using System.ServiceModel;
using WhosHome.Logic;

namespace WhosHome.Communication
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IClient))]
    public interface IServer
    {
        [OperationContract]
        string GetTitle();

        [OperationContract]
        void HandleAction(ObservableCollection<Vehicle> newList);
    }
}