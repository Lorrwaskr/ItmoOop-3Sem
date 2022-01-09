namespace Backups.JobObject
{
    public interface IJobObject<TJobObjectType>
    {
        string Name { get; set; }
        bool IsAvailable();
        TJobObjectType Get();
    }
}