using Model;
using Model.Models;

namespace Library.Models
{
    public class VendorOutModel
    {
        public string Name { get; set; }
        public ContactOutModel Contact { get; set; }

        public VendorOutModel(Vendor vendor)
        {
            Name = vendor.Name;
            Contact = new ContactOutModel(vendor.Contact);
        }
    }
}
