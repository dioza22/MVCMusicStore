using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCMusicStore.Models;
using System.Threading.Tasks;

namespace MVCMusicStore.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        MusicStoreEntities db = new MusicStoreEntities();
        const string PromoCode = "FREE";
        // GET: Checkout/AddressAndPayment
        public ActionResult AddressAndPayment()
        {
            return View();
        }

        // POST Checkout/AddressAndPayment
        [HttpPost]
        public async Task<ActionResult> AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);

            try
            {
                bool promo = (string.Equals(values["PromoCode"], PromoCode, StringComparison.OrdinalIgnoreCase) == false);
                if (promo)
                {
                    return View(order);
                }
                else
                {
                    order.UserName = User.Identity.Name;
                    order.OrderDate = DateTime.Now;

                    //Save Order
                    db.Orders.Add(order);
                    await db.SaveChangesAsync();

                    //Process Order
                    var cart = ShoppingCart.GetCart(this.HttpContext);
                    await cart.CreateOrder(order);

                    return RedirectToAction("Complete", new { id = order.OrderID });
                }

            }
            catch
            {
                //Invalid -- Redisplay with errors
                return View(order);
            }
        }

        // GET: /Checkout/Complete
        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
            bool isValid = db.Orders.Any(order => order.OrderID == id && order.UserName == User.Identity.Name);
            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}