using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
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

            NameRequiredError.Visibility = Visibility.Collapsed;
            var type = VehicleTypeCombo.SelectedItem;

            MainWindow.Instance.AddVehicle(new Vehicle { Name = VehicleNameTxt.Text, Type = ((KeyValuePair<VehicleTypeEnum, string>)type).Key });

            VehicleNameTxt.Text = "";
        }
    }
}
