using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Security;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using WhosHome.Logic;

namespace WhosHome.Communication
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class Client: IClient
    {
        private IServer WhosHomeServer { get; set; }
        public string ServerTitle { get; set; }
        public void UpdateList(ObservableCollection<Vehicle> newList)
        {
            MainWindow.Instance.UpdateList(newList);
        }

        public bool CreateClientSession(string serverIpAdress)
        {
            var binding = new NetTcpBinding
            {
                MaxReceivedMessageSize = Int32.MaxValue,
                Security = new NetTcpSecurity { Message = new MessageSecurityOverTcp { ClientCredentialType = MessageCredentialType.None }, Mode = SecurityMode.None, Transport = new TcpTransportSecurity { ClientCredentialType = TcpClientCredentialType.None, ProtectionLevel = ProtectionLevel.None } },
                ReaderQuotas = new XmlDictionaryReaderQuotas
                    {
                        MaxArrayLength = Int32.MaxValue,
                        MaxBytesPerRead = Int32.MaxValue,
                        MaxDepth = Int32.MaxValue,
                        MaxNameTableCharCount = Int32.MaxValue,
                        MaxStringContentLength = Int32.MaxValue,
                    },
                ReceiveTimeout = TimeSpan.MaxValue,
                SendTimeout = TimeSpan.MaxValue,
            };

            binding.ReliableSession.InactivityTimeout = TimeSpan.MaxValue;

            var channel = new DuplexChannelFactory<IServer>(this, binding, new EndpointAddress(string.Format("net.tcp://{0}:{1}/WhosHomeHost", serverIpAdress, NetworkHelper.GetPort())));

            try
            {
                WhosHomeServer = channel.CreateChannel();
                ((IClientChannel)WhosHomeServer).Open();
                ServerTitle = WhosHomeServer.GetTitle();
                UpdateList(WhosHomeServer.GetList());
            }
            catch (EndpointNotFoundException)
            {
                return false;
            }
            return true;
        }

        public void HandleAction(ObservableCollection<Vehicle> newList)
        {
            try
            {
                WhosHomeServer.HandleAction(newList);
            }
            catch (Exception)
            {

            }
        }
    }
}
