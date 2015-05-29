using System;
using System.ComponentModel.DataAnnotations;


namespace MVCMusicStore.Models
{
    public class Cart
    {
        [Key]
        public int RecordID { get; set; }
        public string CartID { get; set; }
        public int AlbumID { get; set; }
        public int Count { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Album Album { get; set; }
    }
}