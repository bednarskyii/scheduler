using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scheduler.Models;

namespace Scheduler.Services
{
    public interface  IScheduleService
    {
        Task AddObjectToList(SingleDateRecord record);
        Task DeleteObject(Guid id);
        Task<List<SingleDateRecord>> GetAll(object date = null);
    }
}
