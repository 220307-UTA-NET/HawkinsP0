namespace RichardH_P0.BL
{
    public class Order
    {
        public int OrderID { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public int LocationID { get; set; }
        public int UserID { get; set; }
        public double OrderTotal { get; set; }

        public Order()
        {
            this.LocationID = -1;
            this.OrderDate = DateTimeOffset.Now;
            this.LocationID = -1;
            this.UserID = -1;
            this.OrderTotal = -1;
        }
            
        public Order(int OrderID, DateTimeOffset OrderDate, int LocationID, int UserID, double OrderTotal)
        {
            this.OrderID = OrderID;
            this.OrderDate = OrderDate;
            this.LocationID = LocationID;
            this.UserID = UserID;
            this.OrderTotal = OrderTotal;
        }
    }
}
