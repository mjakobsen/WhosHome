using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WhosHome.Logic;
using WhosHome.Views;

namespace WhosHome
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private readonly ObservableCollection<Vehicle> _vehicles = new ObservableCollection<Vehicle>();
        private static MainWindow _mainWindow;

        public ObservableCollection<Vehicle> Vehicles
        {
            get { return _vehicles; }
        }

        public static MainWindow Instance
        {
            get { return _mainWindow; }
        }

        public MainWindow()
        {
            InitializeComponent();
            _mainWindow = this;

            Title = "StatusPanel";

            lbVehicle.ItemsSource = Vehicles;



            LoadVehicles();
        }

        public void AddVehicle(Vehicle vehicle)
        {
            Vehicles.Add(vehicle);
            LocalListBackup.SaveToFile();
        }

        private void ButtonBusy_OnClick(object sender, RoutedEventArgs e)
        {
            ((Vehicle)lbVehicle.SelectedItem).Status = StatusEnum.Busy;
            LocalListBackup.SaveToFile();
        }

        private void ButtonIdle_OnClick(object sender, RoutedEventArgs e)
        {
            ((Vehicle)lbVehicle.SelectedItem).Status = StatusEnum.Free;
            LocalListBackup.SaveToFile();
        }

        private void ButtonReady_OnClick(object sender, RoutedEventArgs a)
        {
            ((Vehicle)lbVehicle.SelectedItem).Status = StatusEnum.Home;
            LocalListBackup.SaveToFile();
        }

        private void ButtonService_OnClick(object sender, RoutedEventArgs e)
        {
            ((Vehicle)lbVehicle.SelectedItem).Status = StatusEnum.Service;
            LocalListBackup.SaveToFile();
        }

        private void ButtonOutOfService_OnClick(object sender, RoutedEventArgs e)
        {
            ((Vehicle)lbVehicle.SelectedItem).Status = StatusEnum.OutOfService;
            LocalListBackup.SaveToFile();
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

        private void DeleteVehicle_OnClick(object sender, RoutedEventArgs e)
        {
            Vehicles.Remove(((Vehicle) lbVehicle.SelectedItem));
            LocalListBackup.SaveToFile();
        }

        private void CreateVehicle_Click(object sender, RoutedEventArgs e)
        {
            var view = new CreateVehicle();
            var lwnd = new Window
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Content = view,
                SizeToContent = SizeToContent.WidthAndHeight,
                ResizeMode = ResizeMode.NoResize
            };

            lwnd.ShowDialog();
        }

        private void LoadVehicles()
        {
            try
            {
                Vehicles.Clear();
                foreach (var vehicle in LocalListBackup.LoadFromFile())
                {
                    Vehicles.Add(vehicle);
                }
            }
            catch (Exception ex)
            {
                Vehicles.Clear();
            }
        }
    }
}
