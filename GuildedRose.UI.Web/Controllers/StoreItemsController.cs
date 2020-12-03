using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using GuildedRose.UI.Web.Models;

namespace GuildedRose.UI.Web.Controllers
{
    public class StoreItemsController : Controller
    {
        StoreItem currentItem;

        public async Task<IActionResult> Index()
        {
            List<StoreItem> storeitems = new List<StoreItem>();

            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync(Environment.GetEnvironmentVariable("STOREAPI")))
                {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                    storeitems = JsonConvert.DeserializeObject<List<StoreItem>>(apiresponse);
                }
            }
            return View(storeitems);
        }

        public async Task<IActionResult> Details(string id)
        {
            string cartid;
            if(!Request.Cookies.TryGetValue("cartid", out cartid))
            {
                cartid = new Random().Next().ToString();
                Response.Cookies.Append("cartid", cartid);
            }

            ViewBag.CartId = cartid;
            StoreItem item = new StoreItem();

            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync(Environment.GetEnvironmentVariable("STOREAPI") + id))
                {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                    currentItem = JsonConvert.DeserializeObject<StoreItem>(apiresponse);
                }
            }

            return View(currentItem);
        }
    }
}