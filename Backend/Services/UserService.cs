using Backend.Model;
using Backend.Repository;
using Backend.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;

        private readonly IAlbumRepository _albumRepository;

        public UserService(IUserRepository userRepo, IAlbumRepository albumRepo)
        {
            _userRepository = userRepo;
            _albumRepository = albumRepo;
        }


        public async Task<IList<UserFavoriteMusic>> GetFavoriteMusics(Guid id)
        {
            return await _userRepository.GetFavoriteMusics(id);
        }

        public async Task<User> AddFavoriteMusic(Guid userId, Guid musicId)
        {
            var music = await _albumRepository.GetMusic(musicId);

            var user = await _userRepository.GetById(userId);

            user.FavoriteMusics.Add(new UserFavoriteMusic()
            {
                Music = music,
                MusicId = music.Id,
                User = user,
                UserId = user.Id
            });

            await _userRepository.Update(user);

            return user;
        }

        public async Task<User> RemoveFavoriteMusic(Guid userId, Guid musicId)
        {
            var music = await _albumRepository.GetMusic(musicId);

            var user = await _userRepository.GetById(userId);

            var favoriteMusic = user.FavoriteMusics.FirstOrDefault(x => x.MusicId == music.Id);
            
            user.FavoriteMusics.Remove(favoriteMusic);

            await _userRepository.Update(user);

            return user;
        }

        public async Task<User> Register(RegisterUserViewModel model)
        {
            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(model.Password)),
                Photo = $"https://robohash.org/{Guid.NewGuid()}.png?bgset=any"
            };

            await _userRepository.Save(user);

            return user;
        }

        public async Task<User> Authenticate(SignInViewModel model)
        {
            var password64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(model.Password));
            var user = await _userRepository.Authenticate(model.Email, password64);

            return user;
        }
    }
}
