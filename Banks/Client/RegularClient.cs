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
        public string Address { get; }
        public string NotificationAddress { get; }
        public string Passport { get; }
        public bool IsSubscribedOnNotifications { get; set; }
        public Guid Id { get; }
        public bool IsTrustworthy()
        {
            return Address == null || Passport == null;
        }

        public void Unsubscribe()
        {
            IsSubscribedOnNotifications = false;
        }
    }
}