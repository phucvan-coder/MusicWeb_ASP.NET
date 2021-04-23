using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MusicStore.Models
{
    public class FavoriteAlbum
    {
        [Key]
        public int FavorId { get; set; }
        public string UserName { get; set; }
        public int AlbumId { get; set; }
        public virtual Album Album { get; set; }
    }
}