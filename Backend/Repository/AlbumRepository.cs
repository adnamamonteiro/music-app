﻿using Backend.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public class AlbumRepository : BaseRepository<Album>, IAlbumRepository
    {
        public AlbumRepository(AppDbContext context) : base(context)
        { 
        }

        public new async Task<IList<Album>> GetAll()
        {
            return await this.Query.Include(x => x.Musics).ToListAsync();
        }

        public async Task<IList<Music>> GetMusicFromAlbum(Guid albumId)
        {
            return await this.Query.Include(x => x.Musics)
                             .Where(x => x.Id == albumId)
                             .SelectMany(x => x.Musics)
                             .ToListAsync();
        }

        public async Task<Music> GetMusic(Guid musicId)
        {
            return await this.Query.Include(x => x.Musics)
                                   .SelectMany(x => x.Musics)
                                   .FirstOrDefaultAsync(m => m.Id == musicId);
        }

        public new async Task<Album> GetById(Guid id)
        {
            return await this.Query.Include(x => x.Musics)
                                   .Where(x => x.Id == id)
                                   .FirstOrDefaultAsync();
        }
    }
       
}
