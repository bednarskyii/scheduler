using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scheduler.Models;

namespace Scheduler.Services
{
    public class SchedulerService :  IScheduleService
    {
        public async Task AddObjectToList(SingleDateRecord record)
        {
            record.Id = Guid.NewGuid();
            await App.Database.SaveItemAsync(record);
        }

        public async Task DeleteObject(Guid id)
        {
            await App.Database.DeleteItemAsync(id);
        }

        public async Task<List<SingleDateRecord>> GetAll(object date)
        {
            DateTime day = (DateTime)date;
            return await App.Database.GetAllAsync(day);
        }
    }
}
