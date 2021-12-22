using System;

namespace Banks.Client
{
    public interface IClient
    {
        string FirstName { get; set; }
        string SecondName { get; set; }
        string Address { get; set; }
        string Passport { get; set; }
        Guid Id { get; set; }
        bool IsTrustworthy();
    }
}