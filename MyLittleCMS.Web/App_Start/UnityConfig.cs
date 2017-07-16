using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using MyLittleCMS.Services;

using System;
using MyLittleCMS.Data.Context;
using MyLittleCMS.Core.Repository;
using MyLittleCMS.Data.Repositories;
using CacheManager.Core;
using System.Web;

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

            var cacheConfig = ConfigurationBuilder.BuildConfiguration(settings =>
            {
                settings
                    .WithSystemRuntimeCacheHandle("inprocess");

            });


            container.RegisterType(
                typeof(ICacheManager<>),
                new ContainerControlledLifetimeManager(),
                new InjectionFactory(
                    (c, t, n) => CacheFactory.FromConfiguration(
                        t.GetGenericArguments()[0], cacheConfig)));
      


            container.RegisterType<IEFContext, EFContext>(new  PerRequestLifetimeManager());
            container.RegisterType<IUnitOfWork, EFUnitOfWork>(new PerRequestLifetimeManager());
            container.RegisterType(typeof(IRepository<>), typeof(EFRepository<>));
            container.RegisterType<ILogService, LogService>();
            container.RegisterType<IMembershipService, MembershipService>();
           
        }

       
    }
}