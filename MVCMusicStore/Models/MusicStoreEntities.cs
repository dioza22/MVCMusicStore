﻿using System.Data.Entity;

namespace MVCMusicStore.Models
{
    public class MusicStoreEntities : DbContext
    {
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Album> Albums { get; set; }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<MVCMusicStore.ViewModels.ShoppingCartViewModel> ShoppingCartViewModels { get; set; }

    }
}