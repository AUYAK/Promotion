using System;
using System.Linq;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebUI.Controllers;
using WebUI.Models;

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
        [TestMethod]
        public void Can_Add_To_Card()
        {
            //Arrange - create mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] { new Product { ProductID = 1, Name = "P1", Category = new ProductCategory { CategoryID = 1, Name = "TestCategory" } } }.AsQueryable());
            //Arrange - create cart
            Cart cart = new Cart();
            //Arrange - create controller
            CartController controller = new CartController(mock.Object, null);
            //Act - add product to card
            controller.AddToCart(cart, 1, null);
            //Assert
            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToArray()[0].Product.ProductID, 1);
        }

        [TestMethod]
        public void Adding_Product_To_Cart_Goes_To_Cart_Screen()
        {
            //Arrange-create mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] { new Product { ProductID = 1, Name = "P1", Category = new ProductCategory { CategoryID = 1, Name = "TestCategory" } } }.AsQueryable());
            //Arrange - create cart
            Cart cart = new Cart();
            //Arrange - create controller
            CartController controller = new CartController(mock.Object, null);
            //Act - add product to card
            RedirectToRouteResult result = controller.AddToCart(cart, 2, "myUrl");
            //Assert
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
        }
        [TestMethod]
        public void Can_View_Cart_Contents()
        {
            //Arrange - create a Cart
            Cart cart = new Cart();
            //Arrange - create controller
            CartController controller = new CartController(null, null);
            //Act - call Index Method
            CartIndexViewModel result = (CartIndexViewModel)controller.Index(cart, "myUrl").ViewData.Model;
            //Assert
            Assert.AreSame(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "myUrl");
        }
        [TestMethod]
        public void Cannot_Checkout_Empty_Cart()
        {
            //Arrange - create mock iorderprocessor
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            //Arrange - create empty cart
            Cart cart = new Cart();
            //Arrange - create shipping details
            ShippingDetails shippingDetails = new ShippingDetails();
            //Arrange create an instance of controller
            CartController target = new CartController(null, mock.Object);
            //Act - Checkout
            ViewResult result = target.CheckOut(cart, shippingDetails);
            //Assert check that the order never passed on to the processor
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never);
            // Assert - check that the method is returning the default view
            Assert.AreEqual("", result.ViewName);
            // Assert - check that we are passing an invalid model to the view
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }
        [TestMethod]
        public void Cannot_Checkout_Invalid_Shipping_Details()
        {
            //Arrange - create test IOrderRepository
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            //Arrange - create Cart
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            //Arrange - create an instance of controller
            CartController controller = new CartController(null,mock.Object);
            //Arrange add an error to the controller
            controller.ModelState.AddModelError("error", "error");
            //Act - try to checkout
            ViewResult result=controller.CheckOut(cart, new ShippingDetails());
            // Assert - check that the order hasn't been passed on to the processor
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never);
            // Assert - check that the method is returning the default view
            Assert.AreEqual("", result.ViewName);
            // Assert - check that we are passing an invalid model to the view
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }
        [TestMethod]
        public void Can_Checkout_And_Submit_Order()
        {
            // Arrange - create a mock order processor
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            // Arrange - create a cart with an item
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            // Arrange - create an instance of the controller
            CartController target = new CartController(null, mock.Object);
            // Act - try to checkout
            ViewResult result = target.CheckOut(cart, new ShippingDetails());
            // Assert - check that the order has been passed on to the processor
            mock.Verify(m =>
            m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Once());
            // Assert - check that the method is returning the Completed view
            Assert.AreEqual("Completed", result.ViewName);
            // Assert - check that we are passing a valid model to the view
            Assert.AreEqual(true, result.ViewData.ModelState.IsValid);
        }
    }
    }