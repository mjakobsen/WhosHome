using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WhosHome.Communication;
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

        public Client Client { get; set; }

        public bool ServerMode { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            _mainWindow = this;

            Title = "StatusPanel"; 

            LoadVehicles();
        }

        public void AddVehicle(Vehicle vehicle)
        {
            Vehicles.Add(vehicle);
            HandleChange();
        }

        private void ButtonBusy_OnClick(object sender, RoutedEventArgs e)
        {
            ((Vehicle)lbVehicle.SelectedItem).Status = StatusEnum.Busy;
            HandleChange();
        }

        private void ButtonIdle_OnClick(object sender, RoutedEventArgs e)
        {
            ((Vehicle)lbVehicle.SelectedItem).Status = StatusEnum.Free;
            HandleChange();
        }

        private void ButtonReady_OnClick(object sender, RoutedEventArgs a)
        {
            ((Vehicle)lbVehicle.SelectedItem).Status = StatusEnum.Home;
            HandleChange();
        }

        private void ButtonService_OnClick(object sender, RoutedEventArgs e)
        {
            ((Vehicle)lbVehicle.SelectedItem).Status = StatusEnum.Service;
            HandleChange();
        }

        private void ButtonOutOfService_OnClick(object sender, RoutedEventArgs e)
        {
            ((Vehicle)lbVehicle.SelectedItem).Status = StatusEnum.OutOfService;
            HandleChange();
        }

        private void EventSetter_OnHandler(object sender, KeyboardFocusChangedEventArgs e)
        {
            ListBoxItem item = (ListBoxItem) sender;
            item.IsSelected = true;
        }

        private void DeleteVehicle_OnClick(object sender, RoutedEventArgs e)
        {
            Vehicles.Remove(((Vehicle) lbVehicle.SelectedItem));
            HandleChange();
        }

        public void UpdateList(ObservableCollection<Vehicle> newList)
        {
            if (!Application.Current.Dispatcher.CheckAccess())
            {
                Application.Current.Dispatcher.Invoke(() => UpdateList(newList));
                return;
            }

            Vehicles.Clear();
            foreach (var vehicle in newList)
            {
                Vehicles.Add(vehicle);
            }
            LocalListBackup.SaveToFile();
        }

        private void HandleChange()
        {
            LocalListBackup.SaveToFile();
            if (ServerMode)
            {

            }
            else
            {
                Client.HandleAction(Vehicles);
            }
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

        void ChoseCommunicationRole()
        {
            var view = new CommunicationRolePicker();
            var lwnd = new Window
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Content = view,
                SizeToContent = SizeToContent.WidthAndHeight,
                ResizeMode = ResizeMode.NoResize
            };
            view.Win = lwnd;
            lwnd.ShowDialog();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            ChoseCommunicationRole();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            var view = new About();
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
