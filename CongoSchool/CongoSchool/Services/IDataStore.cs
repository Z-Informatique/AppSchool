using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CongoSchool.Services
{
    public interface IDataStore<T>
    {
        Task<int> AddItemAsync(T item);
        //Task<bool> UpdateItemAsync(T item);
        Task<int> DeleteItemAsync(T t);
        Task<T> GetItemAsync(int id);
        Task<IEnumerable<T>> GetItemsAsync();
    }
}

