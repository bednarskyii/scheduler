using System;
using Scheduler.Enums;

namespace Scheduler.Models
{
    public class ScheduleRecord
    {
        public string Title { set; get; }
        public string TextBody { set; get; }
        public DateTime ExpirationTime { set; get; } = DateTime.Now;
        public RecordStatuses Status { get; set; } = RecordStatuses.Scheduled;
    }
}
