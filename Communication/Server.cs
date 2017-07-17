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
    class Server: IServer
    {
        List<IClient> _clients = new List<IClient>();
        public string GetTitle()
        {
            var client = OperationContext.Current.GetCallbackChannel<IClient>();
            _clients.Add(client);

            return MainWindow.Instance.Title;
        }

        public void HandleAction(ObservableCollection<Vehicle> newList)
        {
            MainWindow.Instance.UpdateList(newList);
        }
    }
}
