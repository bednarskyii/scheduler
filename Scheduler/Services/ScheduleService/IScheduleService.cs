using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scheduler.Models;

namespace Scheduler.Services
{
    public interface IScheduleService
    {
        Task AddObjectToListAsync(SingleDateRecord record);
        Task DeleteObjectAsync(Guid id);
        Task<List<SingleDateRecord>> GetRecordAsync(DateTime? date = null);
        Task<bool> IsDateHasRecords(DateTime date);
    }
}
