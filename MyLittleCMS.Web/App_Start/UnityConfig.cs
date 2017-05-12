using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using MyLittleCMS.Services;
using System;
using MyLittleCMS.Data.Context;
using MyLittleCMS.Core.Repository;
using MyLittleCMS.Data.Repositories;

namespace MyLittleCMS.Web
{
    public static class UnityConfig
    {

        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();
         
            container.RegisterType<IEFContext, EFContext>(new  PerRequestLifetimeManager());
            container.RegisterType<IUnitOfWork, EFUnitOfWork>();
            container.RegisterType(typeof(IRepository<>), typeof(EFRepository<>));
            container.RegisterType<IMembershipService, MembershipService>();
        }

       
    }
}