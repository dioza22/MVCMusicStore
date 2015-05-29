using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCMusicStore.Models
{
    public partial class ShoppingCart
    {
        MusicStoreEntities db = new MusicStoreEntities();

        string ShoppingCartID { get; set; }

        public const string CartSessionKey = "CartID";

        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartID = cart.GetCartID(context);

            return cart;
        }

        // We're using HttpContextBase to allow access to cookies.
        public string GetCartID(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartID = Guid.NewGuid();

                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartID.ToString();
                }
            }
            
            return context.Session[CartSessionKey].ToString();
        }

        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public async void MigrateCart(string userName)
        {
            var shoppingCart = db.Carts.Where(cart => cart.CartID == ShoppingCartID);

            foreach (Cart cart in shoppingCart)
            {
                cart.CartID = userName;
            }
            await db.SaveChangesAsync();
        }

        //Helper method to simplify shopping cart calls
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Album album)
        {
            //Get the matching cart and album instance
            var cartItem = db.Carts.SingleOrDefault(
                item => item.CartID == ShoppingCartID && item.AlbumID == album.AlbumID);

            if (cartItem == null)
            {
                cartItem = new Cart
                {
                    AlbumID = album.AlbumID,
                    CartID = ShoppingCartID,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                db.Carts.Add(cartItem);
            }
            else
            {
                cartItem.Count++;
            }
            db.SaveChangesAsync();
        }

        public async Task<int> RemoveFromCart(int id)
        {
            var cartItem = db.Carts.SingleOrDefault(
                item => item.CartID == ShoppingCartID && item.RecordID == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    db.Carts.Remove(cartItem);
                }
                //Save Changes
                await db.SaveChangesAsync();
            }
            return itemCount;
        }

        public async void EmptyCart()
        {
            var cartItems = db.Carts.Where(cart => cart.CartID == ShoppingCartID);

            foreach (var cartItem in cartItems)
            {
                db.Carts.Remove(cartItem);
            }
            //Save Changes
            await db.SaveChangesAsync();
        }

        public List<Cart> GetCartItems()
        {
            return db.Carts.Where(cart => cart.CartID == ShoppingCartID).ToList();
        }

        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in db.Carts
                          where cartItems.CartID == ShoppingCartID
                          select (int?)cartItems.Count).Sum();
            // Return 0 if all entries are null
            return count ?? 0;
        }

        public decimal GetTotal()
        {
            // Multiply album price by count of that album to get
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total
            decimal? total = (from cartItems in db.Carts
                              where cartItems.CartID == ShoppingCartID
                              select (int?)cartItems.Count * cartItems.Album.Price).Sum();

            return total ?? Decimal.Zero;
        }

        public async Task<int> CreateOrder(Order order)
        {
            decimal orderTotal = Decimal.Zero;

            var cartItems = GetCartItems();
            // Iterate over the items in the cart, adding the order details for each
            foreach (var cartItem in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    AlbumID = cartItem.AlbumID,
                    OrderID = order.OrderID,
                    Quatity = cartItem.Count,
                    UnitPrice = cartItem.Album.Price
                };

                // Set the order total of the shopping cart
                orderTotal += (cartItem.Count * cartItem.Album.Price);

                db.OrderDetails.Add(orderDetail);
            }
            // Set the order's total to the orderTotal count
            order.Total = orderTotal;

            //Save the order
            await db.SaveChangesAsync();

            //Empty the cart
            EmptyCart();

            // Return the OrderId as the confirmation number
            return order.OrderID;
        }
    }
}