namespace RichardH_P0.BL
{
    public class Item
    {
        public int ID { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public double  ItemPrice { get; set; }

        public Item (int ID, string ItemName, string ItemDescription, double ItemPrice)
        {
            this.ID = ID;
            this.ItemName = ItemName;
            this.ItemDescription = ItemDescription;
            this.ItemPrice = ItemPrice;
        }
    }
}
