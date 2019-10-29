using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<List<SingleDateRecord>> GetAllAsync(DateTime date)
        {
            return await database.Table<SingleDateRecord>().Where( (i) => i.ExpirationTime.Date == date.Date).ToListAsync();
        }

        public Task<int> SaveItemAsync(SingleDateRecord item)
        {
            return database.InsertAsync(item);
        }

         public async Task<int> DeleteItemAsync(Guid id)
        {
            return await database.Table<SingleDateRecord>().Where(i => i.Id == id).DeleteAsync();
        }
    }
}
