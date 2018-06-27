namespace Library.Models
{
    public class InventoryInModel
    {

        public string Guid { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        //vendor ID can be serial number or bar code number
        public string SerialNo { get; set; }

        public string VendorName { get; set; }

        public int Quantity { get; set; }

        public int QuantityAvailable { get; set; }
    }
}
