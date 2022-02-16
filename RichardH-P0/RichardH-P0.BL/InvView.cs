namespace RichardH_P0.BL
{
    public class InvView
    {
        public int InventoryID { get; set; }
        public int ItemID { get; set; }
        public int Quantity { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public double ItemPrice { get; set; }

        public InvView()
        {
            this.InventoryID = 0;
            this.ItemID = 0;
            this.Quantity = 0;
            this.ItemName = "";
            this.ItemDescription = "";
            this.ItemPrice = 0;
        }
        public InvView(int InventoryID, int ItemID, int Quantity, string ItemName, string ItemDescription, double ItemPrice)
        {
            this.InventoryID = InventoryID;
            this.ItemID = ItemID;
            this.Quantity = Quantity;
            this.ItemName = ItemName;
            this.ItemDescription = ItemDescription;
            this.ItemPrice = ItemPrice;
        }
    }
}