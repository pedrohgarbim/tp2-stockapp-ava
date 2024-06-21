using System.Threading.Tasks;
using StockApp.Domain.Entities;

namespace StockApp.Domain.Interfaces
{
    public interface IUserRepository
    {
      
        Task<User> GetByUsernameAsync(string username);
        Task<User> AddAsync(User user); 
        Task<User> GetByIdAsync (int id);
        Task UpdateAsync(User user);

    }
}
