using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using WhosHome.Logic;
using WhosHome.Communication;

namespace WhosHome.Views
{
    /// <summary>
    /// Interaction logic for CommunicationRolePicker.xaml
    /// </summary>
    public partial class CommunicationRolePicker : UserControl, INotifyPropertyChanged
    {
        public CommunicationRolePicker()
        {
            InitializeComponent();
            CurrentIpAdress = NetworkHelper.GetCurrentIpAdress();
            MinHeight = 150;
        }

        public Window Win { get; set; }

        private void ServerBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ServerContentGrid.Visibility = Visibility.Visible;
            ClientContentGrid.Visibility = Visibility.Collapsed;
        }

        private void ClientBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ClientContentGrid.Visibility = Visibility.Visible;
            ServerContentGrid.Visibility = Visibility.Collapsed;
        }

        private string _currentIpAdress;
        public string CurrentIpAdress
        {
            get { return _currentIpAdress; }
            set
            {
                if (value == _currentIpAdress) return;
                _currentIpAdress = value;
                NotifyOfPropertyChange(() => CurrentIpAdress);
            }
        }

        private void StartServer_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ServerNameTxt.Text)) return;
            MainWindow.Instance.Title = ServerNameTxt.Text;
            ServiceHost sh = new ServiceHost(typeof(Server), new[] { new Uri(string.Format("net.tcp://localhost:{0}/", NetworkHelper.GetPort())) });
            var newEndpoint = new NetTcpBinding
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
            //newEndpoint.ReliableSession.Enabled = true;
            newEndpoint.ReliableSession.InactivityTimeout = TimeSpan.MaxValue;
            sh.AddServiceEndpoint(typeof(IServer), newEndpoint, "WhosHomeHost");

            try
            {
                sh.Open();
            }
            catch (Exception)
            {
                Environment.Exit(0);
            }

            MainWindow.Instance.ServerMode = true;

            Win.Close();
        }

        private void StartClient_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ClientIpTxt.Text)) return;

            try
            {
                var client = new Client();

                var result = client.CreateClientSession(ClientIpTxt.Text);

                if (!result) Environment.Exit(0);

                MainWindow.Instance.Client = client;

                MainWindow.Instance.Title = client.ServerTitle;
            }
            catch (Exception)
            {
                Environment.Exit(0);
            }

            Win.Close();
        }

        #region NotifyPropertyChange
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyOfPropertyChange(string name)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        protected void NotifyOfPropertyChange<T>(Expression<Func<T>> selectorExpression)
        {
            if (selectorExpression == null)
                throw new ArgumentNullException("selectorExpression");
            var body = selectorExpression.Body as MemberExpression;
            if (body == null)
                throw new ArgumentException("The body must be a member expression");
            NotifyOfPropertyChange(body.Member.Name);
        }
        #endregion
    }
}
