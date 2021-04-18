using MusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicStore.ViewModels
{
    public class AlbumViewModel
    {
        public Album Albums { get; set; }
        public List<Comment> Comments { get; set; }
        public Comment comment { get; set; }
    }
}