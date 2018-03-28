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
            Product p1 = new Product { Name = "P1",ProductID=1, Price = 12 };
            Product p2 = new Product { Name = "P2",ProductID=2, Price = 18 };
            Product p3 = new Product { Name = "P3",ProductID=3, Price = 14 };
            //Arrange - create cart
            Cart cart = new Cart();
            //Act - add products
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 2);
            cart.AddItem(p3, 0);
            //Assert
            Assert.AreEqual(48,cart.ComputeTotalVal());


        }
    }
}
