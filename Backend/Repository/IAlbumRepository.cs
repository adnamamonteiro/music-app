using Backend.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public interface IAlbumRepository : IBaseRepository<Album>
    {
        new Task<IList<Album>> GetAll();
        Task<IList<Music>> GetMusicFromAlbum(Guid albumId);
        Task<Music> GetMusic(Guid musicId);
        new Task<Album> GetById(Guid id);
    }
}
