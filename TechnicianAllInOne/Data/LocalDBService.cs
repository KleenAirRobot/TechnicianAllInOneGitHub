using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicianAllInOne.Models;

namespace TechnicianAllInOne.Data
{
    public class LocalDBService
    {
        private const string DB_NAME = "demo_local_db,db3";

        //private const string DB_NAME = "kleen,db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDBService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connection.CreateTableAsync<Users>();
            _connection.CreateTableAsync<MissedService>();
            _connection.CreateTableAsync<ChangeReport>();
            _connection.CreateTableAsync<ExpenseReport>();
            //_connection.CreateTableAsync<Images>();
        }
        
        //info for users
        public async Task<List<Users>> GetUsers()
        {
            return await _connection.Table<Users>().ToListAsync();
        }

        public async Task<Users> GetUsersById(int id)
        {
            return await _connection.Table<Users>().Where(x => x.V2_Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Users>> GetUsersByUserName(string username)
        {
            return await _connection.Table<Users>().Where(x => x.V2_UserName == username).OrderByDescending(x => x.V2_Id).ToListAsync();
        }

        public async Task CreateUsers(Users users)
        {
            await _connection.InsertAsync(users);
        }

        public async Task UpdateUsers(Users user)
        {
            await _connection.UpdateAsync(user);
        }

        public async Task DeleteUsers(Users users)
        {
            await _connection.DeleteAsync(users);
        }

        //info for missed service

        public async Task<List<MissedService>> GetService()
        {
            return await _connection.Table<MissedService>().ToListAsync();
        }

        public async Task<List<MissedService>> GetServiceDesc()
        {
            return await _connection.Table<MissedService>().ToListAsync();
        }

        public async Task<List<MissedService>> GetServiceById(int id)
        {
            return await _connection.Table<MissedService>().Where(x => x.V2_UserId == id).OrderByDescending(x=>x.V2_Id).ToListAsync();
        }

        public async Task CreateService(MissedService service)
        {
            await _connection.InsertAsync(service);
        }

        public async Task UpdateService(MissedService service)
        {
            await _connection.UpdateAsync(service);
        }

        public async Task DeleteService(MissedService service)
        {
            await _connection.DeleteAsync(service);
        }

        //info for changed service

        public async Task<List<ChangeReport>> GetChange()
        {
            return await _connection.Table<ChangeReport>().ToListAsync();
        }

        public async Task<List<ChangeReport>> GetChangeDesc()
        {
            return await _connection.Table<ChangeReport>().ToListAsync();
        }

        public async Task<List<ChangeReport>> GetChangeById(int id)
        {
            return await _connection.Table<ChangeReport>().Where(x => x.V2_UserId == id).OrderByDescending(x => x.V2_Id).ToListAsync();
        }

        public async Task CreateChange(ChangeReport service)
        {
            await _connection.InsertAsync(service);
        }

        public async Task UpdateChange(ChangeReport service)
        {
            await _connection.UpdateAsync(service);
        }

        public async Task DeleteChange(ChangeReport service)
        {
            await _connection.DeleteAsync(service);
        }

        //info for expense reports

        public async Task<List<ExpenseReport>> GetExpense()
        {
            return await _connection.Table<ExpenseReport>().ToListAsync();
        }

        public async Task<List<ExpenseReport>> GetExpenseDesc()
        {
            return await _connection.Table<ExpenseReport>().ToListAsync();
        }

        public async Task<List<ExpenseReport>> GetExpenseById(int id)
        {
            return await _connection.Table<ExpenseReport>().Where(x => x.V2_UserId == id).OrderByDescending(x => x.V2_Id).ToListAsync();
        }

        public async Task CreateExpense(ExpenseReport service)
        {
            await _connection.InsertAsync(service);
        }

        public async Task UpdateExpense(ExpenseReport service)
        {
            await _connection.UpdateAsync(service);
        }

        public async Task DeleteExpense(ExpenseReport service)
        {
            await _connection.DeleteAsync(service);
        }

    }

}

