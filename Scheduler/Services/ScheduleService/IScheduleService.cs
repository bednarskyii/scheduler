using System;
using System.Collections.Generic;
using Scheduler.Models;

namespace Scheduler.ScheduleService
{
    public interface  IScheduleService
    {
        void AddObjectToList(ScheduleRecord record);
        void DeleteObject(ScheduleRecord record);
        List<ScheduleRecord> GetAll();
    }
}
