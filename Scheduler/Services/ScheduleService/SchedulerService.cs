using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scheduler.Models;

namespace Scheduler.Services
{
    public class SchedulerService :  IScheduleService
    {
        public async Task AddObjectToListAsync(SingleDateRecord record)
        {
            record.Id = Guid.NewGuid();
            await App.Database.SaveItemAsync(record);
        }

        public async Task DeleteObjectAsync(Guid id)
        {
            await App.Database.DeleteItemByIdAsync(id);
        }

        public async Task<List<SingleDateRecord>> GetRecordAsync(object date)
        {
            return await App.Database.GetRecordsAsync(date);
        }
    }
}
