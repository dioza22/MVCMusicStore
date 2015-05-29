using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Threading.Tasks;
using MVCMusicStore.Models;
using MVCMusicStore.ViewModels;


namespace MVCMusicStore.Controllers
{
    [RequireHttps]
    public class ShoppingCartController : Controller
    {
        MusicStoreEntities db = new MusicStoreEntities();

        // GET: ShoppingCart
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            //Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            //return the view
            return View(viewModel);
        }

        //
        // GET /Store/AddToCart/5

        public ActionResult AddToCart(int id)
        {
            //Retrieve the album from db
            var addedAlbum = db.Albums.Single(a => a.AlbumID == id);

            //Add it to the Shopping Cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.AddToCart(addedAlbum);
            
            //Go back to the main page to continue shopping
            return RedirectToAction("Index");
        }

        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public async Task<ActionResult> RemoveFromCart(int id)
        {
            //Get the item from the context
            var cart = ShoppingCart.GetCart(this.HttpContext);

            //Get the name of the album to display confirmation
            string albumName = db.Albums.Single(a => a.AlbumID == id).Title;

            //Remove album from cart
            int itemCount = await cart.RemoveFromCart(id);

            var results = new ShoppingCartRemoveViewModel{
                Message = Server.HtmlEncode(albumName) + " has been removed from your shopping cart.",
                CartCount = cart.GetCount(),
                CartTotal = cart.GetTotal(),
                DeletedID = id,
                ItemCount = itemCount
            };

            return Json(results);
        }

        //
        // GET: /ShoppingCart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();

            return PartialView("CartSummary");
        }
    }
}