using System;
using Scheduler.Enums;

namespace Scheduler.Models
{
    public class SingleDateRecord
    {
        public Guid Id { get; set; }
        public string Title { set; get; }
        public string TextBody { set; get; }
        public DateTime ExpirationTime { set; get; }
        public RecordStatuses Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
