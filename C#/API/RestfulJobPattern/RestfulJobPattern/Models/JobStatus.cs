using System.Runtime.Serialization;

namespace RestfulJobPattern.Models
{
    public enum JobStatus
    {
        [EnumMember] Queued = 0,
        [EnumMember] Running = 1,
        [EnumMember] Complete = 2
    }
}