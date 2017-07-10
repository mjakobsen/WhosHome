using System.ServiceModel;

namespace WhosHome.Communication
{
    public interface IClient
    {
        [OperationContract]
        void UpdateList();
    }
}