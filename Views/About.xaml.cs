using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WhosHome.Logic;

namespace WhosHome.Views
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : UserControl, INotifyPropertyChanged
    {
        public About()
        {
            InitializeComponent();
            CurrentIpAdress = NetworkHelper.GetCurrentIpAdress();
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
