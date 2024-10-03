using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prueba1_Real.src.Models;

namespace Prueba1_Real.src.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> ExistsByCode(string code);

        Task<User> Insert(User user);

        Task SaveChanges();



        Task<User?> FindByCode(string code);

        Task Delete(User user);

        Task SelectAll();
    }
}