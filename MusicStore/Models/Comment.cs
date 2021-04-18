using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MusicStore.Models
{
    [Authorize]
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        [ScaffoldColumn(false)]
        public int AlbumId { get; set; }

        [ScaffoldColumn(false)]
        public String Username { get; set; }
        public String Description { get; set; }

        public virtual Album Album { get; set; }

    }
}