using System;
using System.Collections.Generic;
namespace GuildedRose.UI.Web.Models

{
    public class CartModel
    {
        public CartModel()
        {
        }

        public string Id { get; set; }
        public List<CartItem> Items { get; set; }
    }
}
