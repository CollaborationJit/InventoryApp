using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.Models
{
    public class InventoryItem
    {
        public enum ItemType
        {
            MONITOR,
            DESKTOP,
            FURNITURE,
            ACCESSORIES,
            VEHICLE,
            PHONE,
            TABLET,
            INVALID
        }

        [Key]
        public Guid Guid{ get; set; }

        public string Title { get; set; }

        public ItemType Type { get; set; }

        //vendor ID can be serial number or bar code number
        public string SerialNo { get; set; }

        public virtual Vendor Vendor { get; set; }

        public int Quantity { get; set; }

        public int QuantityAvailable { get; set; }

        public static ItemType GetItemType(string strType)
        {
            try
            {
                string upperCase = strType.ToUpper();
                ItemType parsedType = (ItemType)Enum.Parse(typeof(ItemType), upperCase);
                return parsedType;
            }catch
            {
                return ItemType.INVALID;
            }
        }

    }
}
