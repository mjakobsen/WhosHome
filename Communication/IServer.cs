using System.ServiceModel;

namespace WhosHome.Communication
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IClient))]
    public interface IServer
    {
        [OperationContract]
        string GetTitle();
    }
}