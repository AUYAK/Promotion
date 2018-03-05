using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebUI.HtmlHelpers;
using WebUI.Models;

namespace UnitTests
{
    [TestClass]
    public class ExtensionsUnitTests
    {
        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            //Arrange - definition of helper
            HtmlHelper myHelper = null;
            //Arrange - create PagingInfo data
            PagingInfo pagingInfo = new PagingInfo { CurrentPage = 2, TotalItems = 28, ItemsPerPage = 10 };
            //Arrange - set up the delegate using a lambda expression
            Func<int, string> pageUrlDelegate = i => "Page" + i;
            //Act
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);
            //Assert
            Assert.AreEqual(result.ToString(), @"<a href=""Page1"">1</a>"
            + @"<a class=""selected"" href=""Page2"">2</a>"
            + @"<a href=""Page3"">3</a>");
        }
    }
}
