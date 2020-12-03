using System;
namespace GuildedRose.UI.Web.Models
{
    public class StoreItem
    {
        public StoreItem()
        {
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string url { get; set; }
        public decimal Rating { get; set; }
        public int InventoryCount { get; set; }
    }
}
