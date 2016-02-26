// // Breeze Project
// // Created by Bartosz Rachwal. 
// // Copyright (c) 2016 Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Breeze.Components.DAL.MongoDB.Model;
using Breeze.Components.Model;

namespace Breeze.Components.DAL.MongoDB.Adapter
{
    public class MongoComponentConverter : IMongoComponentConverter
    {
        public ICoreComponent Convert(MongoCoreComponent mongoCoreComponent)
        {
            var coreComponent = new CoreComponent
            {
                BreezeId = mongoCoreComponent.BreezeId,
                Name = mongoCoreComponent.Name
            };

            var inPorts = new List<Port>();
            for (var i = 0; i < mongoCoreComponent.InPorts.Count; i++)
            {
                var current = mongoCoreComponent.InPorts.ElementAt(i);
                inPorts.Add(new Port {BreezeId = current.BreezeId, Name = current.Name, Value = current.Value});
            }
            coreComponent.InPorts = inPorts;

            var outPorts = new List<Port>();
            for (var i = 0; i < mongoCoreComponent.OutPorts.Count; i++)
            {
                var current = mongoCoreComponent.OutPorts.ElementAt(i);
                outPorts.Add(new Port {BreezeId = current.BreezeId, Name = current.Name, Value = current.Value});
            }
            coreComponent.OutPorts = outPorts;

            return coreComponent;
        }

        public IEnumerable<ICoreComponent> Convert(IEnumerable<MongoCoreComponent> mongoCoreComponents)
        {
            var components = new List<ICoreComponent>();
            for (var i = 0; i < mongoCoreComponents.Count(); i++)
            {
                components.Add(Convert(mongoCoreComponents.ElementAt(i)));
            }
            return components;
        }

        public MongoCoreComponent Convert(ICoreComponent coreComponent)
        {
            var mongoCoreComponent = new MongoCoreComponent
            {
                BreezeId = coreComponent.BreezeId,
                Name = coreComponent.Name
            };

            var inPorts = new List<MongoPort>();
            for (var i = 0; i < coreComponent.InPorts.Count(); i++)
            {
                var current = coreComponent.InPorts.ElementAt(i);
                inPorts.Add(new MongoPort {BreezeId = current.BreezeId, Name = current.Name, Value = current.Value});
            }
            mongoCoreComponent.InPorts = inPorts;

            var outPorts = new List<MongoPort>();
            for (var i = 0; i < coreComponent.OutPorts.Count(); i++)
            {
                var current = coreComponent.OutPorts.ElementAt(i);
                outPorts.Add(new MongoPort {BreezeId = current.BreezeId, Name = current.Name, Value = current.Value});
            }
            mongoCoreComponent.OutPorts = outPorts;

            return mongoCoreComponent;
        }

        public IEnumerable<MongoCoreComponent> Convert(IEnumerable<ICoreComponent> coreComponents)
        {
            var components = new List<MongoCoreComponent>();
            for (var i = 0; i < coreComponents.Count(); i++)
            {
                components.Add(Convert(coreComponents.ElementAt(i)));
            }
            return components;
        }
    }
}