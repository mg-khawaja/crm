using CrmStepMobileApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CrmStepMobileApp.Helper
{
    public class SQLiteDB
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<SQLiteDB> Instance = new AsyncLazy<SQLiteDB>(async () =>
        {
            var instance = new SQLiteDB();
            await Database.CreateTableAsync<Events>();
            await Database.CreateTableAsync<LocalNotificationModel>();
            await Database.CreateTableAsync<LocalUserModel>();
            return instance;
        });

        public SQLiteDB()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        //Events
        public Task<List<Events>> GetEventsAsync()
        {
            return Database.Table<Events>().ToListAsync();
        }
        public Task<Events> GetEventAsync(string id)
        {
            return Database.Table<Events>().Where(i => i.id == id).FirstOrDefaultAsync();
        }
        public Task<int> SaveEventAsync(Events item)
        {
            if (!string.IsNullOrEmpty(item.id))
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }
        public Task<int> InsertAllEventsAsync(List<Events> items)
        {
            return Database.InsertAllAsync(items);
        }
        public Task<int> DeleteEventAsync(Events item)
        {
            return Database.DeleteAsync(item);
        }
        public Task<int> DeleteAllEventsAsync()
        {
            return Database.DeleteAllAsync<Events>();
        }

        //Local Notifications
        public Task<List<LocalNotificationModel>> GetNotificationsAsync()
        {
            return Database.Table<LocalNotificationModel>().ToListAsync();
        }
        public Task<LocalUserModel> GetUserAsync()
        {
            return Database.Table <LocalUserModel>().FirstOrDefaultAsync();
        }
        public Task<int> SaveUserAsync(LocalUserModel user)
        {
            if (user.Id != 0)
            {
                return Database.UpdateAsync(user);
            }
            else
            {
                return Database.InsertAsync(user);
            }
        }
        public Task<int> DeleteAllUsersAsync()
        {
            return Database.DeleteAllAsync<LocalUserModel>();
        }
        public Task<LocalNotificationModel> GetNotificationAsync(int id)
        {
            return Database.Table<LocalNotificationModel>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        public Task<int> SaveNotificationAsync(LocalNotificationModel item)
        {
            if (item.Id != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }
        public Task<int> InsertAllNotificationsAsync(List<LocalNotificationModel> items)
        {
            return Database.InsertAllAsync(items);
        }
        public Task<int> DeleteNotificationAsync(LocalNotificationModel item)
        {
            return Database.DeleteAsync(item);
        }
        public Task<int> DeleteAllNotificationsAsync()
        {
            return Database.DeleteAllAsync<LocalNotificationModel>();
        }
    }
    public class AsyncLazy<T>
    {
        readonly Lazy<Task<T>> instance;

        public AsyncLazy(Func<T> factory)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }

        public AsyncLazy(Func<Task<T>> factory)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }

        public TaskAwaiter<T> GetAwaiter()
        {
            return instance.Value.GetAwaiter();
        }
    }
}
