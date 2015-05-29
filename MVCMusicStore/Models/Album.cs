using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MVCMusicStore.Models
{
    [Bind(Exclude = "AlbumID")]
    public class Album
    {
        [ScaffoldColumn(false)]
        public virtual int AlbumID { get; set; }

        [Display(Name = "Genre")]
        public virtual int GenreID { get; set; }

        [Display(Name = "Artist")]
        public virtual int ArtistID { get; set; }

        [Required(ErrorMessage = "An album title is required")]
        [StringLength(160)]
        public virtual string Title { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 100.00, ErrorMessage = "Price must be between 0.01 and 100.00")]
        public virtual decimal Price { get; set; }

        [Display(Name = "Album Art Url")]
        [StringLength(1024)]
        public virtual string AlbumArtUrl { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}