// // Breeze Project
// // Created by Bartosz Rachwal. 
// // Copyright (c) 2016 Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System.Collections.Generic;
using System.Web.Http;
using Breeze.SystemBuilder.Adapter;
using Breeze.SystemBuilder.Model;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace Breeze.SystemBuilder.WebApi
{
    [Authorize]
    public class SystemBuilderController : ApiController
    {
        private readonly ISystemBuilderAdapter systemBuilder;
        private readonly ISystemBuilderComponentsAdapter systemBuilderComponents;

        public SystemBuilderController(ISystemBuilderAdapter systemBuilderAdapter,
            ISystemBuilderComponentsAdapter systemBuilderComponentsAdapter)
        {
            systemBuilder = systemBuilderAdapter;
            systemBuilderComponents = systemBuilderComponentsAdapter;
        }

        public string Get(string id)
        {
            if (!id.Equals(User.Identity.GetUserId()))
            {
                return "Not authorized";
            }
            var entry = systemBuilder.Retrive(id);
            return entry != null ? entry.Content : string.Empty;
        }

        public string Post(string id, [FromBody] dynamic data)
        {
            if (!id.Equals(User.Identity.GetUserId()))
            {
                return "Not authorized";
            }

            var cells = JsonConvert.DeserializeObject<dynamic>(data).cells;

            var components = new List<ISystemBuilderComponent>();
            //var connections = new List<Link>();

            foreach (var cell in cells)
            {
                var type = cell.type.Value;

                //  if (type.Equals("link"))
                //  {
                //      var link = new Link
                //      {
                //          Id = cell.id.Value,
                //          Source = new Connection { ComponentId = cell.source.id.Value, PortName = cell.source.port.Value },
                //          Target = new Connection { ComponentId = cell.target.id.Value, PortName = cell.target.port.Value }
                //      };
                //      connections.Add(link);
                //  }
                //  else 
                if (type.Equals("devs.Model"))
                {
                    var component = new SystemBuilderComponent
                    {
                        Name = cell.breezeName.Value,
                        BreezeId = cell.breezeId.Value,
                        Color = cell.breezeColor.Value
                    };
                    components.Add(component);
                }
            }

            systemBuilderComponents.Update(id, components);
            systemBuilder.Update(id, data.ToString());
            return "OK";
        }
    }
}