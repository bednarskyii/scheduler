using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Scheduler.Models;
using SQLite;

namespace Scheduler.Data
{
    public class DatabaseRepository : IDatabaseRepository
    {
        private readonly SQLiteAsyncConnection database;
        private readonly string path;

        public DatabaseRepository()
        {
            path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Scheduler.db3");
            database = new SQLiteAsyncConnection(path);
            database.CreateTableAsync<SingleDateRecord>().Wait();
        }

        public async Task DeleteItemByIdAsync(Guid id)
        {
            await database.Table<SingleDateRecord>().Where(i => i.Id == id).DeleteAsync();
        }

        public async Task<List<SingleDateRecord>> GetRecordsAsync(DateTime? date = null, string titlePart = null)
        {
            if (date != null)
            {
                DateTime unboxedDate = (DateTime)date;
                unboxedDate = unboxedDate.Date;
                return await database.Table<SingleDateRecord>().Where((i) => i.ExpirationTime == unboxedDate).ToListAsync();
            }
            if (titlePart != null)
            {
                return await database.Table<SingleDateRecord>().Where((i) => i.Title.Contains(titlePart)).ToListAsync();
            }
            else
            {
                return await database.Table<SingleDateRecord>().ToListAsync();
            }
        }

        public async Task SaveItemAsync(SingleDateRecord item)
        {
            await database.InsertAsync(item);
        }

        public async Task<List<DateTime>> GetAllDatesWithRecordsByMonth(DateTime month)
        {
            DateTime firstDayOfMonth = new DateTime(month.Year, month.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var ObjectsByMonth = await database.Table<SingleDateRecord>().Where((i) => i.ExpirationTime >= firstDayOfMonth && i.ExpirationTime <= lastDayOfMonth).ToListAsync();

            var values = new List<DateTime>();

            foreach(SingleDateRecord currentObj in ObjectsByMonth)
            {
                values.Add((DateTime)currentObj.ExpirationTime);
            }

            //string query = $"SELECT ExpirationTime, Title " +
            //               $"FROM SingleDateRecord " +
            //               $"WHERE ExpirationTime is 12/1/2019 ";
            //               //$"AND ExpirationTime < {lastDayOfMonth.ToString("dd'/'MM'/'yyyy")}" ;// AND {lastDayOfMonth.ToString("dd'/'MM'/'yyyy")}"; 

            //var t = await database.QueryAsync<SingleDateRecord>(query);
            return values;
        }

    }
}
