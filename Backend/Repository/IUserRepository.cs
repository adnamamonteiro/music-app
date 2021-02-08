using Backend.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        new Task<User> GetById(Guid id);
        Task<IList<UserFavoriteMusic>> GetFavoriteMusics(Guid id);
        Task<User> Authenticate(string email, string password);
    }
}
