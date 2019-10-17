using System;
using System.Collections.Generic;

namespace Scheduler.ScheduleService
{
    public interface IScheduleService<T>
    {
        void AddObjectToList(T record);
        void DeleteObject(T record);
        List<T> GetAll();
    }
}
