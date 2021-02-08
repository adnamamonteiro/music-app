using Backend.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {

        }

        public new async Task<User> GetById(Guid id)
        {
            return await this.Query.Include(x => x.FavoriteMusics)
                                   .ThenInclude(x => x.Music)
                                   .ThenInclude(x => x.Album)
                                   .Where(x => x.Id == id)
                                   .FirstOrDefaultAsync();
        }

        public async Task<User> Authenticate(string email, string password)
        {
            return await this.Query.Include(x => x.FavoriteMusics)
                                   .ThenInclude(x => x.Music)
                                   .ThenInclude(x => x.Album)
                                   .Where(x => x.Password == password && x.Email == email)
                                   .FirstOrDefaultAsync();
        }

        public async Task<IList<UserFavoriteMusic>> GetFavoriteMusics(Guid id)
        {
            return await this.Query.Include(x => x.FavoriteMusics)
                                   .ThenInclude(x => x.Music)
                                   .ThenInclude(x => x.Album)
                                   .Where(x => x.Id == id)
                                   .SelectMany(x => x.FavoriteMusics)
                                   .ToListAsync();
        }
    }
}
