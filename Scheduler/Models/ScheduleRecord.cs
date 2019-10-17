using System;
namespace Scheduler.Models
{
    public class ScheduleRecord
    {
        public string Title { set; get; }
        public string TextBody { set; get; }
        public DateTime ExpirationTime { set; get; }
    }
}
