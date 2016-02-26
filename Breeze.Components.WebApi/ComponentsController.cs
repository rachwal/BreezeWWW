// // Breeze Project
// // Created by Bartosz Rachwal. 
// // Copyright (c) 2016 Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System.Web.Http;
using Breeze.Components.Adapter;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace Breeze.Components.WebApi
{
    public class ComponentsController : ApiController
    {
        private readonly ICoreComponentsAdapter adapter;

        public ComponentsController(ICoreComponentsAdapter componentsAdapter)
        {
            adapter = componentsAdapter;
        }

        public string Get(string id)
        {
            if (!id.Equals(User.Identity.GetUserId()))
            {
                return "Not authorized";
            }
            var components = adapter.Get(id);
            var serializedComponents = JsonConvert.SerializeObject(components);
            return serializedComponents;
        }

        public string Post(string id, [FromBody] dynamic data)
        {
            if (!id.Equals(User.Identity.GetUserId()))
            {
                return "Not authorized";
            }
            return "OK";
        }
    }
}