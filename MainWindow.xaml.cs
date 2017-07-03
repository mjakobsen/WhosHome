using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
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

namespace WhosHome
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Vehicle> _vehicles;

        public ObservableCollection<Vehicle> Vehicles
        {
            get { return _vehicles; }
            set { _vehicles = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
            Vehicles = new ObservableCollection<Vehicle>();

            Title = "StatusPanel";
            Vehicles.Add(new Vehicle() { Name = "Ambulance 1", Status = StatusEnum.Home});
            Vehicles.Add(new Vehicle() { Name = "Lægebil 1", Status = StatusEnum.Home});
            Vehicles.Add(new Vehicle() { Name = "Brandbil 1", Status = StatusEnum.Home, Type = VehicleTypeEnum.Fire});
            Vehicles.Add(new Vehicle() { Name = "Ambulance 2", Status = StatusEnum.Home });
            Vehicles.Add(new Vehicle() { Name = "Lægebil 2", Status = StatusEnum.Home });
            Vehicles.Add(new Vehicle() { Name = "Brandbil 2", Status = StatusEnum.Home, Type = VehicleTypeEnum.Fire});
            Vehicles.Add(new Vehicle() { Name = "Ambulance 3", Status = StatusEnum.Home });
            Vehicles.Add(new Vehicle() { Name = "Lægebil 3", Status = StatusEnum.Home });
            Vehicles.Add(new Vehicle() { Name = "Brandbil 3", Status = StatusEnum.Home, Type = VehicleTypeEnum.Fire});
            Vehicles.Add(new Vehicle() { Name = "Ambulance 4", Status = StatusEnum.Home });
            Vehicles.Add(new Vehicle() { Name = "Lægebil 4", Status = StatusEnum.Home });
            Vehicles.Add(new Vehicle() { Name = "Brandbil 4", Status = StatusEnum.Home, Type = VehicleTypeEnum.Fire});
            Vehicles.Add(new Vehicle() { Name = "Ambulance 5", Status = StatusEnum.Home });
            Vehicles.Add(new Vehicle() { Name = "Lægebil 5", Status = StatusEnum.Home });
            Vehicles.Add(new Vehicle() { Name = "Brandbil 5", Status = StatusEnum.Home, Type = VehicleTypeEnum.Fire});
            Vehicles.Add(new Vehicle() { Name = "Ambulance 6", Status = StatusEnum.Home });
            Vehicles.Add(new Vehicle() { Name = "Lægebil 6", Status = StatusEnum.Home });
            Vehicles.Add(new Vehicle() { Name = "Brandbil 6", Status = StatusEnum.Home, Type = VehicleTypeEnum.Fire});
            Vehicles.Add(new Vehicle() { Name = "Ambulance 7", Status = StatusEnum.Home });
            Vehicles.Add(new Vehicle() { Name = "Lægebil 7", Status = StatusEnum.Home });

            lbVehicle.ItemsSource = Vehicles;
        }

        private void ButtonBusy_OnClick(object sender, RoutedEventArgs e)
        {
            ((Vehicle)lbVehicle.SelectedItem).Status = StatusEnum.Busy;
        }

        private void ButtonIdle_OnClick(object sender, RoutedEventArgs e)
        {
            ((Vehicle)lbVehicle.SelectedItem).Status = StatusEnum.Free;
        }

        private void ButtonReady_OnClick(object sender, RoutedEventArgs a)
        {
            ((Vehicle)lbVehicle.SelectedItem).Status = StatusEnum.Home;
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

        private void EventSetter_OnHandler(object sender, KeyboardFocusChangedEventArgs e)
        {
            ListBoxItem item = (ListBoxItem) sender;
            item.IsSelected = true;
        }

        private void ButtonService_OnClick(object sender, RoutedEventArgs e)
        {
            ((Vehicle)lbVehicle.SelectedItem).Status = StatusEnum.Service;
        }

        private void ButtonOutOfService_OnClick(object sender, RoutedEventArgs e)
        {
            ((Vehicle)lbVehicle.SelectedItem).Status = StatusEnum.OutOfService;
        }
    }
}
