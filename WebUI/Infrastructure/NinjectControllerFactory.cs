using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;
using Moq;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebUI.Infrastructure
{
    public class NinjectControllerFactory:DefaultControllerFactory
    {
        private IKernel _ninjectKernel;
        public NinjectControllerFactory() {
            _ninjectKernel = new StandardKernel();
            AddBindings();
        }
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)_ninjectKernel.Get(controllerType);
        }
        private void AddBindings()
        {
            //Mock<IBlurbRepository> mock = new Mock<IBlurbRepository>();
            //mock.Setup(m=>m.Blurbs).Returns(new List<Blurb>{
            //new Blurb{Name="Advertising for vk.com",Description="Created by Pavel Durov" },
            //new Blurb{Name="Advertising of sporty lifestyle", Description="For everyone"},
            //new Blurb{Name="Advertising of strawberry from Luninets", Description="Created by community of grannies from back porch" } }.AsQueryable());
            _ninjectKernel.Bind<IBlurbRepository>().To<EFBlurbRepository>();
        }
    }
}