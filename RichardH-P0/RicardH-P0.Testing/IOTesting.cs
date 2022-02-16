using Xunit;
using System;
using RichardH_P0.App;
using RichardH_P0.BL;
using RichardH_P0.DL;
using Moq;

namespace RicardH_P0.Testing
{
    public class IOTesting
    {
        [Fact]
        public void TrueIsTrue()
        {
            bool result = true;
            var expected = true;
            Assert.Equal(expected, result);
        }

        [Fact]
        public void PrintLoginMenu_CorrectFormat()
        {
            // arrange
            Mock<IRepository> mockRepo = new();

            var io = new IO(mockRepo.Object);

            // act
            string result = io.LoginMenu();

            // assert
            var expected = "Please make your selection from the options below: \r\n---------------------------------------------------------------\r\n[0] - Exit Application\r\n[1] - Create New User\r\n[2] - Login\r\n";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void PrintCustomerMenu_CorrectFormat()
        {
            // arrange
            Mock<IRepository> mockRepo = new();

            var io = new IO(mockRepo.Object);

            // act
            string result = io.CustomerMenu();

            // assert
            var expected = "*** Customer Menu *** \r\nPlease make your option selection below: \r\n---------------------------------------------------------------\r\n[0] - Exit Application\r\n[1] - Choose Store\r\n[2] - Review Store Inventory\r\n[3] - Place New Order\r\n[4] - Review Past Orders\r\n";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void PrintManagerMenu_CorrectFormat()
        {
            // arrange
            Mock<IRepository> mockRepo = new();

            var io = new IO(mockRepo.Object);

            // act
            string result = io.ManagerMenu();

            // assert
            var expected =
            "*** Management Menu ***\r\nPlease make your option selection below: \r\n---------------------------------------------------------------\r\n[0] - Exit Application\r\n[1] - Choose Store\r\n[2] - Review Store Inventory\r\n[3] - Review All Orders\r\n[4] - Review Order by Customer\r\n[5] - Update Store Inventory\r\n[6] - Add Item to Invntory\r\n[7] - Change Location Sale Percentage\r\n";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void PrintUsers_CorrectFormat()
        {
            // arrange
            Mock<IRepository> mockRepo = new();

            mockRepo.Setup(x => x.GetAllUsers()).Returns(Array.Empty<User>());

            var io = new IO(mockRepo.Object);

            // act
            string result = io.PrintUsers();    

            // assert
            var expected = "UserID\tUserName\tDefault Location\tManager\r\n---------------------------------------------------------------\r\n---------------------------------------------------------------\r\n";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void PrintLocations_CorrectFormat()
        {
            // arrange
            Mock<IRepository> mockRepo = new();

            mockRepo.Setup(x => x.GetAllLocations()).Returns(Array.Empty<Location>());

            var io = new IO(mockRepo.Object);

            // act
            string result = io.PrintLocations();

            // assert
            var expected = "ID\tLocation Name\tSale Percentage\r\n---------------------------------------------------------------\r\n---------------------------------------------------------------\r\n";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void PrintItems_CorrectFormat()
        {
            // arrange
            Mock<IRepository> mockRepo = new();

            mockRepo.Setup(x => x.GetAllItems()).Returns(Array.Empty<Item>());

            var io = new IO(mockRepo.Object);

            // act
            string result = io.PrintItems();

            // assert
            var expected = "ID\tItem Name\tItem Description\r\n---------------------------------------------------------------\r\n---------------------------------------------------------------\r\n";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void PrintInventories_CorrectFormat()
        {
            // arrange
            Mock<IRepository> mockRepo = new();

            mockRepo.Setup(x => x.GetAllInventories()).Returns(Array.Empty<Inventory>());

            var io = new IO(mockRepo.Object);

            // act
            string result = io.PrintInventories();

            // assert
            var expected = "InventoryID\tItemID\tItemPrice\tLocationID\tQuantity\r\n---------------------------------------------------------------\r\n---------------------------------------------------------------\r\n";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void PrintLocationInventory_CorrectFormat()
        {
            // arrange
            int LocationID = 1;
            Mock<IRepository> mockRepo = new();

            mockRepo.Setup(x => x.GetLocationSalePercentage(LocationID)).Returns(100);
            mockRepo.Setup(x => x.GetLocationInventory(LocationID)).Returns(Array.Empty<InvView>());

            var io = new IO(mockRepo.Object);

            // act
            string result = io.PrintLocationInventory(LocationID);

            // assert
            var expected = "ItemId\tItem Name\tUnit Price\tQuantity\r\n---------------------------------------------------------------\r\n---------------------------------------------------------------\r\n";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void PrintOrders_CorrectFormat()
        {
            // arrange
            Mock<IRepository> mockRepo = new();

            mockRepo.Setup(x => x.GetAllOrders()).Returns(Array.Empty<Order>());

            var io = new IO(mockRepo.Object);

            // act
            string result = io.PrintOrders();

            // assert
            var expected = "OrderID\tOrder Date\t\tUser\t\tLocation\t\tOrder Total\r\n---------------------------------------------------------------\r\n---------------------------------------------------------------\r\n";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void PrintUserOrders_CorrectFormat()
        {
            // arrange
            Mock<IRepository> mockRepo = new();

            mockRepo.Setup(x => x.GetUserOrders(1)).Returns(Array.Empty<Order>());
            mockRepo.Setup(x => x.GetUserName(1)).Returns("");

            var io = new IO(mockRepo.Object);

            // act
            string result = io.PrintUserOrders(1);

            // assert
            var expected = "Displaying order history for :\r\nOrderID\tOrder Date\t\tOrder Total\r\n---------------------------------------------------------------\r\n---------------------------------------------------------------\r\n";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void PrintOrderDetail_CorrectFormat()
        {       
            // arrange
            Mock<IRepository> mockRepo = new();

            mockRepo.Setup(x => x.GetOrderDetail(1)).Returns(Array.Empty<OrderItem>());

            var io = new IO(mockRepo.Object);

            // act
            string result = io.PrintOrderDetail(1);

            // assert
            var expected = "Displaying order history for Order: 1\r\nItem\tQuantity\tPrice\r\n---------------------------------------------------------------\r\n---------------------------------------------------------------\r\n";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetLocation_ReturnsLocaiton()
        {
            // arrange
            User NewUser = new(5, "UserName", "Password", 1, true);
            Location Location1 = new(1, "New York", 100);
            Mock<IRepository> mockRepo = new();

            mockRepo.Setup(x => x.GetLocation(NewUser)).Returns(Location1);
            var io = new IO(mockRepo.Object);

            // act
            Location result = io.GetLocation(NewUser);

            // assert
            Location expected = Location1;
            Assert.Equal(expected, result);
        }
    }
}