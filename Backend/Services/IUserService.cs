using Backend.Model;
using Backend.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Services
{
    public interface IUserService
    {
        Task<IList<UserFavoriteMusic>> GetFavoriteMusics(Guid id);
        Task<User> AddFavoriteMusic(Guid userId, Guid musicId);
        Task<User> RemoveFavoriteMusic(Guid userId, Guid musicId);
        Task<User> Register(RegisterUserViewModel model);
        Task<User> Authenticate(SignInViewModel model);
    }
}
