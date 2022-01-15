namespace BackupsExtra.Logger
{
    public interface ILogger
    {
        bool Timecode { get; set; }
        void Write(string message);
    }
}