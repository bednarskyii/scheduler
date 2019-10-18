using System;
using System.Collections.Generic;
using Scheduler.Models;

namespace Scheduler.ScheduleService
{
    public interface  IScheduleService<T>
    {
        void AddObjectToList(T record);
        void DeleteObject(T record);
        List<ScheduleRecord> GetAll();
    }
}
