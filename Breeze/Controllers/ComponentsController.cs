// // Breeze Project
// // Created by Bartosz Rachwal. 
// // Copyright (c) 2016 Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System;
using System.Web.Mvc;
using Breeze.Components.Adapter;
using Breeze.Components.Model;
using Breeze.SystemBuilder.Adapter;
using Breeze.SystemBuilder.Model;
using Microsoft.AspNet.Identity;

namespace Breeze.Controllers
{
    public class ComponentsController : Controller
    {
        private readonly ICoreComponentsAdapter coreComponents;
        private readonly ISystemBuilderComponentsAdapter systemBuilderComponents;

        public ComponentsController(ICoreComponentsAdapter coreComponentsAdapter,
            ISystemBuilderComponentsAdapter systemBuilderAdapter)
        {
            coreComponents = coreComponentsAdapter;
            systemBuilderComponents = systemBuilderAdapter;
        }

        public ActionResult Index()
        {
            var id = User.Identity.GetUserId();
            var components = systemBuilderComponents.Get(id);
            return View(components);
        }

        public ActionResult Add()
        {
            var component = new CoreComponent();
            return View(component);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(CoreComponent model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            for (var i = 0; i < model.InPorts.Count; i++)
            {
                model.InPorts[i].BreezeId = Guid.NewGuid().ToString();
                model.InPorts[i].Value = string.Empty;
            }

            for (var i = 0; i < model.OutPorts.Count; i++)
            {
                model.OutPorts[i].BreezeId = Guid.NewGuid().ToString();
                model.OutPorts[i].Value = string.Empty;
            }

            var id = User.Identity.GetUserId();

            systemBuilderComponents.Update(id, new SystemBuilderComponent
            {
                BreezeId = model.BreezeId,
                Name = model.Name,
                InBuilder = false,
                Color = "#00a65a"
            });

            coreComponents.Update(id, model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit()
        {
            return View();
        }
    }
}