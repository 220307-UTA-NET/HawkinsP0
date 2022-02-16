using System;
using RichardH_P0.BL;

namespace RichardH_P0.DL
{
    public interface IRepository
    {
        public string GetLine()
        {
            return Console.ReadLine();
        }
        IEnumerable<User> GetAllUsers();
        string GetUserName(int UserID);
        IEnumerable<Location> GetAllLocations();
        string GetLocationName(int LocationID);
        Item GetItem(int ItemID);
        IEnumerable<Item> GetAllItems();
        IEnumerable<Inventory> GetAllInventories();
        IEnumerable<InvView> GetLocationInventory(int LocationID);
        int GetLocationSalePercentage(int LocationID);
        int GetItemQuantity(int LocationID, int ItemID);
        int CreateItem(string ItemName, string ItemDescription, double Price);
        void AddInventory(int LocationID, int ItemID, int Quantity);
        void UpdateInventory(int LocationID, int ItemID, int Quantity);
        IEnumerable<Order> GetAllOrders();
        int CreateOrderAndGetOrderID(int LocationID, int UserId, double price);
        IEnumerable<Order> GetUserOrders(int UserID);
        IEnumerable<OrderItem> GetOrderDetail(int OrderID);
        int CreateUserAndReturnUserID(User NewUser);
        User LookupUser(string Username, string Password);
        void SelectLocation(User CurrentUser);
        Location GetLocation(User CurrentUser);
        InvView GetInvView(int ItemID, int LocationID);
        void SetLocationSalePercentage(Location CurrentLocation, int SalePercentage);
        void CreateOrderItem(OrderItem orderItem);
        int GetItemQuantityFromLocation(int LocationID, int ItemID);
    }
}