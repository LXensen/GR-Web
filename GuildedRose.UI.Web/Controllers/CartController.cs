using System;
using System.Dynamic;
using System.Text;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using GuildedRose.UI.Web.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GuildedRose.UI.Web.Controllers
{
    public class CartController : Controller
    {
        CartModel cartmodel;
        List<InventoryItem> inventoryitem;
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            dynamic models = new ExpandoObject();

            string cartid = Request.Cookies["cartid"];
            StringBuilder sb = new StringBuilder();

            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync(Environment.GetEnvironmentVariable("CARTAPI") + cartid))
                {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                    cartmodel = JsonConvert.DeserializeObject<CartModel>(apiresponse);
                }

                models.Cart = cartmodel;

                foreach(CartItem item in cartmodel.Items)
                {
                    sb.Append("id=" + item.Id);
                }

                using (var response = await httpclient.GetAsync(Environment.GetEnvironmentVariable("INVENTORYAPI") + "Items?" + sb.ToString()))
                {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                    inventoryitem = JsonConvert.DeserializeObject<List<InventoryItem>>(apiresponse);
                }

                models.Inventory = inventoryitem;
            }

            return View(models);
        }

        public IActionResult Checkout()
        {
            return View();
        }
    }
}
