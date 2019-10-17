using System;
using System.Collections.Generic;

namespace Scheduler.ScheduleService
{
    public class SchedulerService<T> :  IScheduleService<T>
    {

        private readonly List<T> _listOfObjectsMoq = new List<T>();

        public void AddObjectToList(T record)
        {
            _listOfObjectsMoq.Add(record);
        }

        public void DeleteObject(T record)
        {
            _listOfObjectsMoq.Remove(record);
        }

        public List<T> GetAll()
        {
            return _listOfObjectsMoq;
        }
    }
}
