// // Breeze Project
// // Created by Bartosz Rachwal. 
// // Copyright (c) 2016 Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System.Web.Mvc;
using Breeze.SystemBuilder.Adapter;
using Microsoft.AspNet.Identity;

namespace Breeze.Controllers
{
    public class SystemBuilderController : Controller
    {
        private readonly ISystemBuilderComponentsAdapter adapter;

        public SystemBuilderController(ISystemBuilderComponentsAdapter componentsAdapter)
        {
            adapter = componentsAdapter;
        }

        public ActionResult Index()
        {
            var id = User.Identity.GetUserId();
            var components = adapter.Get(id);
            return View(components);
        }
    }
}