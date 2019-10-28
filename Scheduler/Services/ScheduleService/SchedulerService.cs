using System;
using System.Collections.Generic;
using Scheduler.Models;

namespace Scheduler.ScheduleService
{
    public class SchedulerService :  IScheduleService
    {
        public void AddObjectToList(ScheduleRecord record)
        {
            CancerDatabase.listOfObjectsMoq.Add(record);
        }

        public void DeleteObject(ScheduleRecord record)
        {
            CancerDatabase.listOfObjectsMoq.Remove(record);
        }

        public List<ScheduleRecord> GetAll()
        {
            return CancerDatabase.listOfObjectsMoq;
        }
    }
}
