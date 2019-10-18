using System;
using System.Collections.Generic;
using Scheduler.Models;

namespace Scheduler.ScheduleService
{
    public class SchedulerService<T> :  IScheduleService<T>
    {
        public void AddObjectToList(T record)
        {
            CancerDatabase.listOfObjectsMoq.Add(record as ScheduleRecord);
        }

        public void DeleteObject(T record)
        {
            CancerDatabase.listOfObjectsMoq.Remove(record as ScheduleRecord);
        }

        public List<ScheduleRecord> GetAll()
        {
            return CancerDatabase.listOfObjectsMoq;
        }
    }
}
