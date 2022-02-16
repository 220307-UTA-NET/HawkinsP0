namespace RichardH_P0.BL
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int LocationID { get; set; }
        public bool IsManager { get; set; }
        
        public User()
        {
            this.Id = -1;
            this.Username = "";
            this.Password = "";
            this.LocationID = -1;
            this.IsManager = false;
        }

        public User(int Id, string Username, string Password, int LocationId = 0, bool IsManager = false)
        {
            this.Id = Id;
            this.Username = Username;
            this.Password = Password;
            this.LocationID = LocationId;
            this.IsManager = IsManager;
        }
    }


}