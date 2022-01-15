using System;

namespace Banks.Client
{
    public interface IClient
    {
        string FirstName { get; }
        string SecondName { get; }
        string Address { get; }
        string NotificationAddress { get; }
        string Passport { get; }
        bool IsSubscribedOnNotifications { get; }
        Guid Id { get; }
        bool IsTrustworthy();
        void Unsubscribe();
        void ChangeAddress(string newAddress);
        void ChangePassport(string newPassport);
        void ChangeNotificationAddress(string newAddress);
    }
}