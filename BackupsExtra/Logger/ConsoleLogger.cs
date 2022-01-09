using System;

namespace BackupsExtra.Logger
{
    public class ConsoleLogger : ILogger
    {
        public bool Timecode { get; set; }
        public void Write(string message)
        {
            if (Timecode)
                Console.WriteLine(DateTime.Now + ": " + message);
            else
                Console.WriteLine(message);
        }
    }
}