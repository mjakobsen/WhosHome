﻿using System.Collections.ObjectModel;
using System.ServiceModel;
using WhosHome.Logic;

namespace WhosHome.Communication
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IServer))]
    public interface IClient
    {
        [OperationContract]
        void UpdateList(ObservableCollection<Vehicle> newList);
    }
}