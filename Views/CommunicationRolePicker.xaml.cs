using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
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
            MainWindow.Instance.RealTtitle = ServerNameTxt.Text;
            ServiceHost sh = new ServiceHost(typeof(Server), new[] { new Uri(string.Format("net.tcp://localhost:14654/")) });
            sh.Open();
            Win.Close();
        }

        private void StartClient_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ClientIpTxt.Text)) return;
            
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
