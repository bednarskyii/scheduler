using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scheduler.Models;

namespace Scheduler.Data
{
    public interface IDatabaseRepository
    {
        Task<List<SingleDateRecord>> GetRecordsAsync(DateTime? date = null, string titlePart = null);
        Task SaveItemAsync(SingleDateRecord item);
        Task DeleteItemByIdAsync(Guid id);
        Task<List<DateTime>> GetAllDatesWithRecordsByMonth(DateTime month);
    }
}
