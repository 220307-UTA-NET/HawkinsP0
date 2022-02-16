using RichardH_P0.BL;
using System.Data.SqlClient;

namespace RichardH_P0.DL
{
    public class SqlRepository : IRepository
    {
        private readonly string _connectionString;

        public SqlRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public int CreateUserAndReturnUserID(User NewUser)
        {
            int intManager = Convert.ToInt32(NewUser.IsManager);

            using SqlConnection connection = new(_connectionString);
            connection.Open();

            using SqlCommand cmd = new(
                @"INSERT INTO Users (UserName, UserPassword, LocationID, Manager)
                    VALUES
                        (@UserName, @Password, @LocationID, @Manager);
                  SELECT UserID
                    FROM Users
                    WHERE UserName = @UserName;",
                connection);

            cmd.Parameters.AddWithValue("@UserName", NewUser.Username);
            cmd.Parameters.AddWithValue("@Password", NewUser.Password);
            cmd.Parameters.AddWithValue("@LocationID", NewUser.LocationID);
            cmd.Parameters.AddWithValue("@Manager", intManager);

            using SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            return reader.GetInt32(0);
        }

        public IEnumerable<User> GetAllUsers()
        {
            List<User> result = new();

            using SqlConnection connection = new(_connectionString);
            connection.Open();

            using SqlCommand cmd = new(
                @"SELECT UserID, UserName, UserPassword, LocationID, Manager
                  FROM Users;",
                connection);

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int Id = reader.GetInt32(0);
                string UserName = reader.GetString(1);
                string Password = reader.GetString(2);
                int LocationId = reader.GetInt32(3);
                bool Manager = Convert.ToBoolean(reader.GetInt32(4));

                result.Add(new(Id, UserName, Password, LocationId, Manager));
            }

            connection.Close();

            return result;
        }

        public string GetUserName(int UserID)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            using SqlCommand cmd = new(
                @"SELECT UserName
                    FROM Users
                    WHERE UserID = @UserID;",
                connection);

            cmd.Parameters.AddWithValue("@UserID", UserID);
            using SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            return reader.GetString(0);
        }

        public IEnumerable<Location> GetAllLocations()
        {
            List<Location> result = new();

            using SqlConnection connection = new(_connectionString);
            connection.Open();

            using SqlCommand cmd = new(
                @"SELECT LocationID, LocationName, SalePercentage
                    FROM Locations;",
                connection);

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int Id = reader.GetInt32(0);
                string LocationName = reader.GetString(1);
                int SalePercentage = reader.GetInt32(2);

                result.Add(new(Id, LocationName, SalePercentage));
            }

            connection.Close();

            return result;
        }

        public string GetLocationName(int LocationID)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            using SqlCommand cmd = new(
                @"SELECT LocationName
                    FROM Locations
                    WHERE LocationID = @LocationID;",
                connection);

            cmd.Parameters.AddWithValue("@LocationID", LocationID);
            using SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            return reader.GetString(0);
        }

        public IEnumerable<Item> GetAllItems()
        {
            List<Item> result = new();

            using SqlConnection connection = new(_connectionString);
            connection.Open();

            using SqlCommand cmd = new(
                @"SELECT ItemID, ItemName, ItemDescription
                  FROM Items;",
                connection);

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int Id = reader.GetInt32(0);
                string ItemName = reader.GetString(1);
                string ItemDescription = reader.GetString(2);
                double ItemPrice = (double)reader.GetDecimal(3);

                result.Add(new(Id, ItemName, ItemDescription, ItemPrice));
            }

            connection.Close();

            return result;
        }

        public IEnumerable<Inventory> GetAllInventories()
        {
            List<Inventory> result = new();

            using SqlConnection connection = new(_connectionString);
            connection.Open();

            using SqlCommand cmd = new(
                @"SELECT Inventories.InventoryID, Items.ItemID, Items.ItemPrice, Inventories.LocationID, Inventories.Quantity
                    FROM Inventories
                    JOIN Items ON Items.ItemID = Inventories.ItemID;",
                connection);

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int InventoryID = reader.GetInt32(0);
                int ItemID = reader.GetInt32(1);
                double ItemPrice = (double)reader.GetDecimal(2);
                int LocationID = reader.GetInt32(3);
                int Quantity = reader.GetInt32(4);

                result.Add(new(InventoryID, ItemID, LocationID, Quantity));
            }

            connection.Close();

            return result;
        }

        public IEnumerable<InvView> GetLocationInventory(int LocationID)
        {
            List<InvView> result = new();

            using SqlConnection connection = new(_connectionString);
            connection.Open();

            using SqlCommand cmd = new(
                @"SELECT InventoryID, Inventories.ItemID, Quantity, ItemName, ItemDescription, ItemPrice
                    FROM Inventories
                    JOIN Items ON Items.ItemID = Inventories.ItemID
                    WHERE LocationID = @LocationID;",
                connection);

            cmd.Parameters.AddWithValue("@LocationID", LocationID);

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int InventoryID = reader.GetInt32(0);
                int ItemID = reader.GetInt32(1);
                int Quantity = reader.GetInt32(2);
                string ItemName = reader.GetString(3);
                string ItemDescription = reader.GetString(4);
                double ItemPrice = (double)reader.GetDecimal(5);

                result.Add(new(InventoryID, ItemID, Quantity, ItemName, ItemDescription, ItemPrice));
            }

            connection.Close();

            return result;
        }

        public int GetLocationSalePercentage(int LocationID)
        {
            int result;

            using SqlConnection connection = new(_connectionString);
            connection.Open();

            using SqlCommand cmd = new(
                @"SELECT SalePercentage
                    FROM Locations
                    WHERE LocationID = @LocationID;",
                connection);

            cmd.Parameters.AddWithValue("@LocationID", LocationID);

            using SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            result = reader.GetInt32(0);

            return result;
        }

        public Item GetItem (int ItemID)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            using SqlCommand cmd = new(
                @"SELECT *
                    FROM Items
                    WHERE ItemID = @ItemID;",
                connection);

            cmd.Parameters.AddWithValue("@ItemID", ItemID);

            using SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            
            int Id = reader.GetInt32(0);
            string ItemName = reader.GetString(1);
            string ItemDescription = reader.GetString(2);
            double ItemPrice = (double)reader.GetDecimal(3);

            return new(Id, ItemName, ItemDescription, ItemPrice);
        }

        public int GetItemQuantity(int LocationID, int ItemID)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            using SqlCommand cmd = new(
                @"SELECT Quantity
                    FROM Inventories
                    WHERE ItemID = @ItemID AND LocationID = @LocationID;",
                connection);

            cmd.Parameters.AddWithValue("@ItemID", ItemID);
            cmd.Parameters.AddWithValue("@LocationID", LocationID);

            using SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            return reader.GetInt32(0);
        }

        public int CreateItem(string ItemName, string ItemDescription, double ItemPrice)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            using SqlCommand cmd = new(
                @"INSERT Items
                    (ItemName, ItemDescription, ItemPrice)
                 VALUES
                    (@ItemName, @ItemDescription, @ItemPrice);

                    SELECT ItemID
                    FROM Items
                    WHERE ItemName = @ItemName AND ItemDescription = @ItemDescription AND ItemPrice = @ItemPrice;",
                connection);

            cmd.Parameters.AddWithValue("@ItemName", ItemName);
            cmd.Parameters.AddWithValue("@ItemDescription", ItemDescription);
            cmd.Parameters.AddWithValue("@ItemPrice", ItemPrice);

            using SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            return reader.GetInt32(0);
        }

        public void AddInventory(int LocationID, int ItemID, int Quantity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            using SqlCommand cmd = new(
                @"Insert Inventories
                    (ItemID, LocationID, Quantity)
                    Values
                    (@ItemID, @LocationID, @Quantity);",
                connection);

            cmd.Parameters.AddWithValue("@LocationID", LocationID);
            cmd.Parameters.AddWithValue("@ItemID", ItemID);
            cmd.Parameters.AddWithValue("@Quantity", Quantity);

            using SqlDataReader reader = cmd.ExecuteReader();
            return;
        }

        public void UpdateInventory(int LocationID, int ItemID, int Quantity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            using SqlCommand cmd = new(
                @"UPDATE Inventories
                    SET Quantity = @Quantity
                    WHERE ItemID = @ItemID AND LocationID = @LocationID;",
                connection);

            cmd.Parameters.AddWithValue("@LocationID", LocationID);
            cmd.Parameters.AddWithValue("@ItemID", ItemID);
            cmd.Parameters.AddWithValue("@Quantity", Quantity);

            using SqlDataReader reader = cmd.ExecuteReader();
            return;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            List<Order> result = new();

            using SqlConnection connection = new(_connectionString);
            connection.Open();

            using SqlCommand cmd = new(
                @"SELECT OrderID, OrderDate, OrderLocationID, UserID, OrderTotal
                  FROM Orders;",
                connection);

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int OrderID = reader.GetInt32(0);
                DateTimeOffset OrderDate = reader.GetDateTimeOffset(1);
                int OrderLocationID = reader.GetInt32(2);
                int UserID = reader.GetInt32(3);
                double OrderTotal = (double)reader.GetDecimal(4);


                result.Add(new(OrderID, OrderDate, OrderLocationID, UserID, OrderTotal));
            }

            connection.Close();

            return result;
        }

        public int CreateOrderAndGetOrderID(int LocationID, int UserID, double price)
        {
            DateTimeOffset Time = DateTimeOffset.Now;

            using SqlConnection connection = new(_connectionString);
            connection.Open();

            using SqlCommand cmd = new(
                @"INSERT INTO Orders
                        (OrderDate, OrderLocationID, UserID, OrderTotal)
                    VALUES
                        (@Time, @LocationID, @UserID, @Price);

                    SELECT OrderID
                    FROM Orders
                    WHERE OrderDate = @Time AND  OrderLocationID = @LocationID AND UserID = @UserID AND OrderTotal = @Price;",
                connection);

            cmd.Parameters.AddWithValue("@Time", Time);
            cmd.Parameters.AddWithValue("@LocationID", LocationID);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@Price", price);

            using SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            return reader.GetInt32(0);
        }

        public IEnumerable<Order> GetUserOrders(int UserID)
        {
            List<Order> result = new();

            using SqlConnection connection = new(_connectionString);
            connection.Open();

            using SqlCommand cmd = new(
                @"SELECT OrderID, OrderDate, OrderLocationID, OrderTotal
                  FROM Orders
                  WHERE UserID = @UserID;",
                connection);

            cmd.Parameters.AddWithValue("@UserID", UserID);

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int OrderID = reader.GetInt32(0);
                DateTimeOffset OrderDate = reader.GetDateTimeOffset(1);
                int OrderLocationID = reader.GetInt32(2);
                double OrderTotal = (double)reader.GetDecimal(3);


                result.Add(new(OrderID, OrderDate, OrderLocationID, UserID, OrderTotal));
            }

            connection.Close();

            return result;
        }

        public IEnumerable<OrderItem> GetOrderDetail(int OrderID)
        {
            List<OrderItem> result = new();

            using SqlConnection connection = new(_connectionString);
            connection.Open();

            using SqlCommand cmd = new(
                @"SELECT OrderItems.OrderID, Items.ItemID, Items.ItemName, OrderItems.Quantity, OrderItems.Price
                    FROM OrderItems
                    JOIN Items ON Items.ItemID = OrderItems.ItemID
                    WHERE OrderID = @orderID;",
                connection);

            cmd.Parameters.AddWithValue("@OrderID", OrderID);

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int ItemID = reader.GetInt32(1);
                string ItemName = reader.GetString(2);
                int Quantity  = reader.GetInt32(3);
                double Price = (double)reader.GetDecimal(4);


                result.Add(new(OrderID, ItemID, Quantity, Price, ItemName));
            }

            connection.Close();

            return result;
        }


        public User LookupUser(string Username, string Password)
        {            
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            using SqlCommand cmd = new(
                @"SELECT UserID, UserName, UserPassword, LocationID, Manager
                    FROM Users
                    WHERE UserName = @UserName AND UserPassword = @Password;",
                connection);

            cmd.Parameters.AddWithValue("@UserName", Username);
            cmd.Parameters.AddWithValue("@Password", Password);

            using SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            return new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), Convert.ToBoolean(reader.GetInt32(4)));
        }

        public void SelectLocation(User CurrentUser)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            using SqlCommand cmd = new(
                @"UPDATE Users
                  SET LocationID = @LocationID
                  WHERE UserID = @UserID;",
                connection);

            cmd.Parameters.AddWithValue("@UserID", CurrentUser.Id);
            cmd.Parameters.AddWithValue("@LocationID", CurrentUser.LocationID);

            using SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
        }

        public Location GetLocation(User CurrentUser)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            using SqlCommand cmd = new(
                @"SELECT LocationID, LocationName, SalePercentage
                  FROM Locations
                  WHERE LocationID = @LocationID;",
                connection);

            cmd.Parameters.AddWithValue("@LocationID", CurrentUser.LocationID);

            using SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            return new Location(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
        }

        public InvView GetInvView(int ItemID, int LocationID)
        {
            InvView result = new();

            using SqlConnection connection = new(_connectionString);
            connection.Open();

            using SqlCommand cmd = new(
                @"SELECT InventoryID, Inventories.ItemID, Quantity, ItemName, ItemDescription, ItemPrice
                    FROM Inventories
                    JOIN Items ON Items.ItemID = Inventories.ItemID
                    WHERE Inventories.ItemID = @ItemID AND Inventories.LocationID = @LocationID;",
                connection);

            cmd.Parameters.AddWithValue("@ItemID", ItemID);
            cmd.Parameters.AddWithValue("@LocationID", LocationID);

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int InventoryID = reader.GetInt32(0);
                int Quantity = reader.GetInt32(2);
                string ItemName = reader.GetString(3);
                string ItemDescription = reader.GetString(4);
                double ItemPrice = (double)reader.GetDecimal(5);

                result = new(InventoryID, ItemID, Quantity, ItemName, ItemDescription, ItemPrice);
            }

            connection.Close();

            return result;
        }

        public void SetLocationSalePercentage(Location CurrentLocation, int SalePercentage)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            using SqlCommand cmd = new(
                @"UPDATE Locations
                    SET SalePercentage = @SalePercentage
                    WHERE LocationID = @LocationID;",
                connection);

            cmd.Parameters.AddWithValue("@LocationID", CurrentLocation.ID);
            cmd.Parameters.AddWithValue("@SalePercentage", SalePercentage);

            using SqlDataReader reader = cmd.ExecuteReader();
            return;
        }

        public void CreateOrderItem(OrderItem orderItem)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            using SqlCommand cmd = new(
                @"Insert OrderItems
                    (OrderID, ItemID, Quantity, Price)
                    Values
                    (@OrderID, @ItemID, @Quantity, @Price);",
                connection);

            cmd.Parameters.AddWithValue("@OrderID", orderItem.OrderID);
            cmd.Parameters.AddWithValue("@ItemID", orderItem.ItemID);
            cmd.Parameters.AddWithValue("@Quantity", orderItem.Quantity);
            cmd.Parameters.AddWithValue("@Price", orderItem.Price);

            using SqlDataReader reader = cmd.ExecuteReader();
            return;
        }

        public int GetItemQuantityFromLocation(int LocationID, int ItemID)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            using SqlCommand cmd = new(
                @"SELECT Quantity
                    FROM Inventories
                    WHERE ItemID = @ItemID AND LocationID = @LocationID;",
                connection);

            cmd.Parameters.AddWithValue("@ItemID", ItemID);
            cmd.Parameters.AddWithValue("@LocationID", LocationID);

            using SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            return reader.GetInt32(0);
        }
    }
}
