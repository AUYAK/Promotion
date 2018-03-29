using System;
using System.Linq;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class CartTest
    {
        [TestMethod]
        public void Can_add_new_lines()
        {
            //Arrange - create sonme tests products
            Product p1 = new Product { Name = "P1", ProductID = 1 };
            Product p2 = new Product { Name = "P2", ProductID = 2 };
            //Arrange - create cart
            Cart cart = new Cart();
            //Act - add Products
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            //Assert
            Cartline[] result = cart.Lines.ToArray();
            Assert.AreEqual(result[0].Product, p1);
            Assert.AreEqual(result[1].Product, p2);
        }
        [TestMethod]
        public void Can_compute_total_val()
        {
            //Arrange - create some tests products
            Product p1 = new Product { Name = "P1", ProductID = 1, Price = 12 };
            Product p2 = new Product { Name = "P2", ProductID = 2, Price = 18 };
            Product p3 = new Product { Name = "P3", ProductID = 3, Price = 14 };
            //Arrange - create cart
            Cart cart = new Cart();
            //Act - add products
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 2);
            cart.AddItem(p3, 0);
            //Assert
            Assert.AreEqual(48, cart.ComputeTotalVal());
        }
        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            // Arrange - create some test products
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            // Arrange - create a new cart
            Cart target = new Cart();
            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);
            Cartline[] results = target.Lines.OrderBy(c => c.Product.ProductID).ToArray();
            // Assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Quantity, 11);
            Assert.AreEqual(results[1].Quantity, 1);
        }
        [TestMethod]
        public void Can_remove_from_cart()
        {
            //Arrange-create some test products
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "p2" };
            //Arrange - create cart
            Cart target = new Cart();
            //Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            //Act - remove one Product From cart
            target.RemoveAll(p1);
            //Assert
            Cartline[] result = target.Lines.ToArray();
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(p2, result[0].Product);
        }
        [TestMethod]
        public void Can_Clear_Contents()
        {
            // Arrange - create some test products
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };
            // Arrange - create a new cart
            Cart target = new Cart();
            // Arrange - add some items
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            // Act - reset the cart
            target.Clear();
            // Assert
            Assert.AreEqual(target.Lines.Count(), 0);
        }
    }
}