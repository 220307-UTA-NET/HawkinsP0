namespace RichardH_P0.BL
{
    public class Location
    {
        public int ID { get; set; }
        public string LocationName { get; set; }
        public int SalePercentage { get; set; }

        public Location (int ID, string LocationName, int SalePercentage)
        {
            this.ID = ID;
            this.LocationName = LocationName;
            this.SalePercentage = SalePercentage;
        }
    }
}
