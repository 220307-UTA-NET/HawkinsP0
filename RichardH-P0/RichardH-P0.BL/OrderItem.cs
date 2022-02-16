namespace RichardH_P0.BL
{
    public class OrderItem
    {
        public int OrderID { get; set; }
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public OrderItem(int ItemID, int Quantity, double Price)
        {
            this.ItemID = ItemID;
            this.Quantity = Quantity;
            this.Price = Price;
            this.OrderID = -1;
            this.ItemName = "";
        }
        public OrderItem(int OrderID, int ItemID, int Quantity, double Price, string ItemName = "")
        {
            this.OrderID = OrderID;
            this.ItemID = ItemID;
            this.ItemName = ItemName;
            this.Quantity = Quantity;
            this.Price = Price;
        }
    }
}
