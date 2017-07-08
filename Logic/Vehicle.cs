using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Xml.Serialization;

namespace WhosHome.Logic
{
    public class Vehicle : INotifyPropertyChanged
    {
        private string _name;
        private StatusEnum _status;
        private VehicleTypeEnum _type;

        public Vehicle()
        {
            Type = VehicleTypeEnum.Other;
            Status = StatusEnum.Home;
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        public StatusEnum Status
        {
            get { return _status; }
            set
            {
                if (_status == value) return;
                _status = value;
                NotifyOfPropertyChange(() => Status);
            }
        }

        public VehicleTypeEnum Type
        {
            get { return _type;}
            set
            {
                if (_type == value) return;
                _type = value;
                NotifyOfPropertyChange(() => Type);
            }
        }

        public bool IsFire { get { return Type == VehicleTypeEnum.Fire; } }

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