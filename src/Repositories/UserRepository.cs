using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prueba1_Real.src.Interfaces;
using Prueba1_Real.src.Models;
using Microsoft.EntityFrameworkCore;
using Prueba1_Real.src.Data;

namespace Prueba1_Real.src.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDBContext _dataContext;

        public UserRepository(AppDBContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> ExistsByCode(string rut)
        {
            return await _dataContext.Users.FirstOrDefaultAsync(p => p.Rut == rut) != null;

        }

        public async Task<User> Insert(User user)
        {
            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();
            return user;
        }

        public async Task SaveChanges()
        {
            await _dataContext.SaveChangesAsync();
        }

        public async Task SelectAll()
        {
            await _dataContext.Users.ToListAsync();
        }

        public async Task<User?> FindByCode(string rut)
        {
            return await _dataContext.Users.FirstOrDefaultAsync(p => p.Rut == rut)!;
        }

        public async Task Delete(User user)
        {
            _dataContext.Users.Remove(user);
            await _dataContext.SaveChangesAsync(); // Asegúrate de guardar los cambios
        }
    }
}