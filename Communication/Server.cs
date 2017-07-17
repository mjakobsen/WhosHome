using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WhosHome.Logic;

namespace WhosHome.Communication
{
    public delegate void OnMainWindowVehiclesChangedHandler();
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    class Server: IServer
    {
        List<IClient> _clients = new List<IClient>();

        public Server()
        {
            MainWindow.Instance.OnMainWindowVehiclesChanged += InstanceOnOnMainWindowVehiclesChanged;
        }

        public string GetTitle()
        {
            var client = OperationContext.Current.GetCallbackChannel<IClient>();
            _clients.Add(client);

            return MainWindow.Instance.Title;
        }

        public ObservableCollection<Vehicle> GetList()
        {
            return MainWindow.Instance.Vehicles;

        }

        private void InstanceOnOnMainWindowVehiclesChanged()
        {
            if (_clients.Count > 0)
            {
                foreach (var client in _clients)
                {
                    client.UpdateList(MainWindow.Instance.Vehicles);
                }
            }
        }

        public void HandleAction(ObservableCollection<Vehicle> newList)
        {
            MainWindow.Instance.UpdateList(newList);
        }
    }
}
