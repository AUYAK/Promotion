using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Domain.Abstract;
using Domain.Entities;
using System.Linq;
using WebUI.Controllers;
using WebUI.Models;
using System;

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
           BlurbsListViewModel result = (BlurbsListViewModel)controller.List(null,2).Model;
            //Assert
            Blurb[] blurbs = result.Blurbs.ToArray();
            Assert.AreEqual(blurbs[0].Name, "Some Blurb 4");
            Assert.AreEqual(blurbs[1].Name, "Some Blurb 5");
            Assert.AreEqual(blurbs[2].Name, "Some Blurb 6");



        }
        [TestMethod]
        public void Generate_Category_Specific_Blurb_Count()
        {
            //Arrange
            //Create Moq repository
            Mock<IBlurbRepository> mock = new Mock<IBlurbRepository>();
            mock.Setup(m=>m.Blurbs).Returns(new Blurb[] {
            new Blurb {BlurbID = 1, Name = "B1", CategoryID = 1, Category=new BlurbCategory(){CategoryID=1, Name="1" },DateOfCreate=DateTime.Now},
            new Blurb {BlurbID = 2, Name = "B1", CategoryID = 1, Category=new BlurbCategory(){CategoryID=1, Name="1" },DateOfCreate=DateTime.Now},
            new Blurb {BlurbID = 3, Name = "B3", CategoryID = 3, Category=new BlurbCategory(){CategoryID=1, Name="3" },DateOfCreate=DateTime.Now},
            new Blurb {BlurbID = 4, Name = "B4", CategoryID = 2, Category=new BlurbCategory(){CategoryID=1, Name="2" },DateOfCreate=DateTime.Now},
            new Blurb {BlurbID = 5, Name = "B5", CategoryID = 2, Category=new BlurbCategory(){CategoryID=1, Name="2" },DateOfCreate=DateTime.Now} }.AsQueryable());
            //Arrange
            //Create controller
            BlurbsController target = new BlurbsController(mock.Object);
            target.Pagesize = 3;
            //Action - test for different categories
            var count1 = ((BlurbsListViewModel)target.List("1").Model).pagingInfo.TotalItems;
            var count2 = ((BlurbsListViewModel)target.List("2").Model).pagingInfo.TotalItems;
            var count3 = ((BlurbsListViewModel)target.List("3").Model).pagingInfo.TotalItems;
            var countall = ((BlurbsListViewModel)target.List(null).Model).pagingInfo.TotalItems;
            //Assert
            Assert.AreEqual(count1, 2);
            Assert.AreEqual(count2, 2);
            Assert.AreEqual(count3, 1);
            Assert.AreEqual(countall, 5);

        }
    }
}
