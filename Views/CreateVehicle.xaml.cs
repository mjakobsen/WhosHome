using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WhosHome.Logic;

namespace WhosHome.Views
{
    /// <summary>
    /// Interaction logic for CreateVehicle.xaml
    /// </summary>
    public partial class CreateVehicle : UserControl
    {
        public ObservableCollection<KeyValuePair<VehicleTypeEnum, string>> VehicleTypeValues
        {
            get;
        }

        public CreateVehicle()
        {
            InitializeComponent();

            VehicleTypeValues = new ObservableCollection<KeyValuePair<VehicleTypeEnum, string>>();
            VehicleTypeValues.Add(new KeyValuePair<VehicleTypeEnum, string>(VehicleTypeEnum.Fire, "Brand"));
            VehicleTypeValues.Add(new KeyValuePair<VehicleTypeEnum, string>(VehicleTypeEnum.Other, "Andet"));

            VehicleTypeCombo.ItemsSource = VehicleTypeValues;

        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(VehicleNameTxt.Text))
            {
                NameRequiredError.Visibility = Visibility.Visible;
                return;
            }

            var type = VehicleTypeCombo.SelectedItem;

            MainWindow.Instance.AddVehicle(new Vehicle { Name = VehicleNameTxt.Text, Type = ((KeyValuePair<VehicleTypeEnum, string>)type).Key });
        }
    }
}
