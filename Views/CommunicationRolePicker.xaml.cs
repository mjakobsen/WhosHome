using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WhosHome.Views
{
    /// <summary>
    /// Interaction logic for CommunicationRolePicker.xaml
    /// </summary>
    public partial class CommunicationRolePicker : UserControl
    {
        public CommunicationRolePicker()
        {
            InitializeComponent();
        }

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
    }
}
