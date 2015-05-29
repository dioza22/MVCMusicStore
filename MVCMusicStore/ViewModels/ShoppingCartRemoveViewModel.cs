using System.ComponentModel.DataAnnotations;

namespace MVCMusicStore.ViewModels
{
    public class ShoppingCartRemoveViewModel
    {
        [Key]
        [ScaffoldColumn(false)]
        public string Message { get; set; }
        public decimal CartTotal { get; set; }
        public int CartCount { get; set; }
        public int ItemCount { get; set; }
        public int DeletedID { get; set; }
    }
}