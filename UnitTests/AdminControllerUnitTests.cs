using Domain.Abstract;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebUI.Controllers;

namespace UnitTests
{
    [TestClass]
    public class AdminControllerUnitTests
    {
        [TestMethod]
        public void Index_Returns_All_Products()
        {
            //Arrange - create mock
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Products).Returns(new Product[] { new Product { ProductID = 1, Name = "P1"},new Product {ProductID=2,Name="P2"}, new Product {ProductID=3,Name="P3"}}.AsQueryable());
            //Act
            AdminController target = new AdminController(mock.Object);
            Product[] result = ((IEnumerable<Product>)target.Index().Model).ToArray();
            //Assert block
            Assert.AreEqual(result.Length,3);
            Assert.AreEqual("P1", result[0].Name);
            Assert.AreEqual("P2", result[1].Name);
            Assert.AreEqual("P3", result[2].Name);
        }
        [TestMethod]
        public void Can_Edit_Product()
        {
            //Arrange - create and setup mock
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] { new Product { ProductID = 1, Name = "P1" }, new Product { ProductID = 2, Name = "P2" }, new Product { ProductID = 3, Name = "P3" } }.AsQueryable());
            //Arrange - create AdminController
            AdminController controller = new AdminController(mock.Object);
            //Act - get product
            Product product = (Product)controller.Edit(2).Model;
            //Assert
            Assert.AreEqual(2, product.ProductID);
            Assert.AreEqual("P2", product.Name);

        }
        [TestMethod]
        public void Cannot_Edit_NonExistent_Product()
        {
            //Arrange - create and setup mock
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] { new Product { ProductID = 1, Name = "P1" }, new Product { ProductID = 2, Name = "P2" }, new Product { ProductID = 3, Name = "P3" } }.AsQueryable());
            //Arrange - create AdminController
            AdminController controller = new AdminController(mock.Object);
            //Act - get product
            Product product = (Product)controller.Edit(5).Model;
            //Assert
            Assert.IsNull(product);
        }
    }
}
