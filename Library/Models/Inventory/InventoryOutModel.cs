using Model;
using Model.Models;

namespace Library.Models.Inventory
{
    public class InventoryOutModel
    {
        public string Guid { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        //vendor ID can be serial number or bar code number
        public string SerialNo { get; set; }

        public VendorOutModel Vendor { get; set; }

        public int Quantity { get; set; }

        public int QuantityAvailable { get; set; }

        public InventoryOutModel(InventoryItem item, ApplicationDbContext _context)
        {

        }
    }
}
