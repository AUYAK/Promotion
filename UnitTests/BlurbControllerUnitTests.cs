using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Domain.Abstract;
using Domain.Entities;
using System.Linq;
using WebUI.Controllers;
using WebUI.Models;

namespace UnitTests
{
    [TestClass]
    public class BlurbControllerUnitTests
    {
        [TestMethod]
        public void Can_Paginate()
        {
            //Arrange
            Mock<IBlurbRepository> mock = new Mock<IBlurbRepository>();
            mock.Setup(m => m.Blurbs).Returns(new Blurb[]{
                new Blurb { Name = "Some Blurb 1", BlurbID = 1 },
                new Blurb { Name = "Some Blurb 2", BlurbID = 2 },
                new Blurb { Name = "Some Blurb 3", BlurbID = 3 },
                new Blurb { Name = "Some Blurb 4", BlurbID = 4 },
                new Blurb { Name = "Some Blurb 5", BlurbID = 5 },
                new Blurb { Name = "Some Blurb 6", BlurbID = 6 }
            }.AsQueryable());
            BlurbsController controller = new BlurbsController(mock.Object);
            controller.Pagesize = 3;
            //Act
           BlurbsListViewModel result = (BlurbsListViewModel)controller.List(2).Model;
            //Assert
            Blurb[] blurbs = result.Blurbs.ToArray();
            Assert.AreEqual(blurbs[0].Name, "Some Blurb 4");
            Assert.AreEqual(blurbs[1].Name, "Some Blurb 5");
            Assert.AreEqual(blurbs[2].Name, "Some Blurb 6");



        }
    }
}
