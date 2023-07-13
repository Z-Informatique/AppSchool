using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CongoSchool.Models;
using SQLite;

namespace CongoSchool.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly SQLiteAsyncConnection database;

        public MockDataStore(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Item>().Wait();
        }

        public async Task<int> AddItemAsync(Item item)
        {
            if (item.Id != 0)
            {
                return await database.UpdateAsync(item);
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }

        public async Task<int> DeleteItemAsync(Item item)
        {
            return await database.DeleteAsync(item);
        }

        public async Task<Item> GetItemAsync(int id)
        {
            return await database.Table<Item>()
                .Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await database.Table<Item>().ToListAsync();
        }
    }
}
