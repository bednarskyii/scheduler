using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Scheduler.Models;
using SQLite;

namespace Scheduler.Data
{
    public class ItemDatabase
    {
        readonly SQLiteAsyncConnection database;

        public ItemDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<SingleDateRecord>().Wait();
        }

        public async Task<List<SingleDateRecord>> GetRecordsAsync(object date = null)
        {
            if (date != null)
            {
                DateTime unboxedDate = (DateTime)date;
                unboxedDate = unboxedDate.Date;
                return await database.Table<SingleDateRecord>().Where( (i) => i.ExpirationTime == unboxedDate ).ToListAsync();
            }
            else
            {
                return await database.Table<SingleDateRecord>().ToListAsync();
            }
        }

        public Task<int> SaveItemAsync(SingleDateRecord item)
        {
            return database.InsertAsync(item);
        }

         public async Task<int> DeleteItemByIdAsync(Guid id)
        {
            return await database.Table<SingleDateRecord>().Where(i => i.Id == id).DeleteAsync();
        }

        public async Task<bool> IsDateHasRecords(DateTime date)
        {
            int t = await database.Table<SingleDateRecord>().Where((i) => i.ExpirationTime == date).CountAsync();

            if (t == 0)
                return false;

            else
                return true;
        }
    }
}
