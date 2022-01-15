using System;
using Banks.Bank;
using Banks.Client;
using Banks.Notification;

namespace Banks.Terminal
{
    public class ConsoleTerminal : RegularClient
    {
        public ConsoleTerminal(string firstName, string secondName, string notificationAddress = null, bool isSubscribedOnNotifications = true, string address = null, string passport = null)
            : base(firstName, secondName, notificationAddress, isSubscribedOnNotifications, address, passport)
        {
        }

        public void LaunchTerminal()
        {
            const string INFO = "Available commands:" +
                                "ChangeClientInfo : " +
                                "                 ChangeAddress" +
                                "                 ChangePassport" +
                                "                 ChangeNotificationAddress" +
                                "                 Unsubscribe" +
                                "GetClientInfo : " +
                                "                 FirstName" +
                                "                 SecondName" +
                                "                 Address" +
                                "                 NotificationAddress" +
                                "                 Passport" +
                                "                 Subscription" +
                                "Help : to get available commands" +
                                "Exit : to exit";
            Console.WriteLine(INFO);

            while (true)
            {
                switch (Console.ReadLine())
                {
                    case "ChangeClientInfo":
                        switch (Console.ReadLine())
                        {
                            case "ChangeAddress":
                                ChangeAddress(Console.ReadLine());
                                break;
                            case "ChangePassport":
                                ChangePassport(Console.ReadLine());
                                break;
                            case "ChangeNotificationAddress":
                                ChangeNotificationAddress(Console.ReadLine());
                                break;
                            case "Unsubscribe":
                                Unsubscribe();
                                break;
                            default:
                                Console.WriteLine("Invalid command");
                                break;
                        }

                        break;
                    case "GetClientInfo":
                        switch (Console.ReadLine())
                        {
                            case "FirstName":
                                Console.WriteLine(FirstName);
                                break;
                            case "SecondName":
                                Console.WriteLine(SecondName);
                                break;
                            case "Address":
                                Console.WriteLine(Address);
                                break;
                            case "NotificationAddress":
                                Console.WriteLine(NotificationAddress);
                                break;
                            case "Passport":
                                Console.WriteLine(Passport);
                                break;
                            case "Subscription":
                                Console.WriteLine(IsSubscribedOnNotifications);
                                break;
                            default:
                                Console.WriteLine("Invalid command");
                                break;
                        }

                        break;
                    case "Help":
                        Console.WriteLine(INFO);
                        break;
                    case "Exit":
                        return;
                    default:
                        Console.WriteLine("Invalid command");
                        break;
                }
            }
        }
    }
}