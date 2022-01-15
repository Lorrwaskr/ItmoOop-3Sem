using System;

namespace Banks.Client
{
    public class RegularClient : IClient
    {
        public RegularClient(string firstName, string secondName, string notificationAddress = null, bool isSubscribedOnNotifications = true, string address = null, string passport = null)
        {
            FirstName = firstName;
            SecondName = secondName;
            NotificationAddress = notificationAddress;
            IsSubscribedOnNotifications = isSubscribedOnNotifications;
            Address = address;
            Passport = passport;
            Id = Guid.NewGuid();
        }

        public string FirstName { get; }
        public string SecondName { get; }
        public string Address { get; private set; }
        public string NotificationAddress { get; private set; }
        public string Passport { get; private set; }
        public bool IsSubscribedOnNotifications { get; private set; }
        public Guid Id { get; }
        public bool IsTrustworthy()
        {
            return Address == null || Passport == null;
        }

        public void Unsubscribe()
        {
            IsSubscribedOnNotifications = false;
        }

        public void ChangeAddress(string newAddress)
        {
            Address = newAddress;
        }

        public void ChangePassport(string newPassport)
        {
            Passport = newPassport;
        }

        public void ChangeNotificationAddress(string newAddress)
        {
            NotificationAddress = newAddress;
        }
    }
}