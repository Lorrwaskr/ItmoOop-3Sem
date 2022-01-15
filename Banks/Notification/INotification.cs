using Banks.Client;

namespace Banks.Notification
{
    public interface INotification
    {
        void Notify(IClient client, string address, string message = "");
    }
}