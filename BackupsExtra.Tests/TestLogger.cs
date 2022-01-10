using System;
using BackupsExtra.Logger;

namespace BackupsExtra.Tests
{
    public class TestLogger : ILogger
    {
        private string log = "";
        public bool Timecode { get; set; }
        public void Write(string message)
        {
            if (Timecode)
            {
                log = log.Insert(log.Length, $"{DateTime.Now} {message}");
            }
            else
            {
                log = log.Insert(log.Length, $"{message}");
            }
        }
    }
}