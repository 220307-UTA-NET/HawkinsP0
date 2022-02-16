namespace RichardH_P0.BL
{
    public class Inventory
    {
        public int InventoryID { get; set; }
        public int ItemID { get; set; }
        public int LocationID { get; set; }
        public int Quantity { get; set; }


        public Inventory(int InventoryID, int ItemID, int LocationID, int Quantity)
        {
            this.InventoryID = InventoryID;
            this.ItemID = ItemID;
            this.LocationID = LocationID;
            this.Quantity = Quantity;
        }
    }
}
