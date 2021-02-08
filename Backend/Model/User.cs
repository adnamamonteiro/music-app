using System;
using System.Collections.Generic;

namespace Backend.Model
{
    public class User
    {
        public User()
        {
            this.FavoriteMusics = new List<UserFavoriteMusic>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Photo { get; set; }

        public IList<UserFavoriteMusic> FavoriteMusics { get; set; }
    }
}
