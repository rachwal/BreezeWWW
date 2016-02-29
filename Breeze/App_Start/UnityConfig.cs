// // Breeze Project
// // Created by Bartosz Rachwal. 
// // Copyright (c) 2016 Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System;
using Breeze.Components.Adapter;
using Breeze.Components.DAL.MongoDB.Adapter;
using Breeze.Controllers;
using Breeze.SystemBuilder.Adapter;
using Breeze.SystemBuilder.DAL.MongoDB.Adapter;
using Microsoft.Practices.Unity;

namespace Breeze
{
    public class UnityConfig
    {
        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return Container.Value;
        }

        private static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());

            container.RegisterType<ISystemBuilderComponentsAdapter, MongoSystemBuilderComponentsAdapter>();
            container.RegisterType<ISystemBuilderAdapter, MongoSystemBuilderAdapter>();
            container.RegisterType<SystemBuilderController>();

            container.RegisterType<IMongoComponentConverter, MongoComponentConverter>();
            container.RegisterType<ICoreComponentsAdapter, MongoCoreComponentsAdapter>();
            container.RegisterType<ComponentsController>();
        }
    }
}