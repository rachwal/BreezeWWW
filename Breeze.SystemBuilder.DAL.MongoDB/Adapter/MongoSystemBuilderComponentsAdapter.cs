// // Breeze Project
// // Created by Bartosz Rachwal. 
// // Copyright (c) 2016 Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using Breeze.SystemBuilder.Adapter;
using Breeze.SystemBuilder.DAL.MongoDB.Model;
using Breeze.SystemBuilder.Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Breeze.SystemBuilder.DAL.MongoDB.Adapter
{
    public class MongoSystemBuilderComponentsAdapter : ISystemBuilderComponentsAdapter
    {
        private readonly IMongoDatabase db;

        public MongoSystemBuilderComponentsAdapter()
        {
            var client = new MongoClient("mongodb://localhost");
            db = client.GetDatabase("SystemBuilder");
        }

        private IMongoCollection<MongoUserSystemBuilderComponents> Components
            => db.GetCollection<MongoUserSystemBuilderComponents>("UserComponents");

        public IEnumerable<ISystemBuilderComponent> Get(string userId)
        {
            var components = Components.Find(new BsonDocument("_id", userId)).FirstOrDefault();
            return components == null
                ? new List<ISystemBuilderComponent>()
                : new List<ISystemBuilderComponent>(components.Components);
        }

        public void Update(string userId, ISystemBuilderComponent component)
        {
            var mongoComponent = new MongoSystemBuilderComponent(component);

            var entry = Components.Find(e => e.UserId == userId).FirstOrDefault();
            if (entry == null)
            {
                Components.InsertOne(new MongoUserSystemBuilderComponents
                {
                    UserId = userId,
                    Components = {mongoComponent}
                });
            }
            else
            {
                var index = entry.Components.FindIndex(c => c.BreezeId == component.BreezeId);
                if (index < 0)
                {
                    entry.Components.Add(mongoComponent);
                }
                else
                {
                    entry.Components[index] = mongoComponent;
                }
                Components.ReplaceOne(e => e.UserId == userId, entry);
            }
        }

        public void Update(string userId, IEnumerable<ISystemBuilderComponent> components)
        {
            for (var i = 0; i < components.Count(); i++)
            {
                Update(userId, components.ElementAt(i));
            }
        }
    }
}