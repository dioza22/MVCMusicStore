using System.Collections.Generic;
using MVCMusicStore.Models;
using System.ComponentModel.DataAnnotations;

namespace MVCMusicStore.ViewModels
{
    public class ShoppingCartViewModel
    {
        [Key]
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}