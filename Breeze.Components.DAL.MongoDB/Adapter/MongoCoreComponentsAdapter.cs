// // Breeze Project
// // Created by Bartosz Rachwal. 
// // Copyright (c) 2016 Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using Breeze.Components.Adapter;
using Breeze.Components.DAL.MongoDB.Model;
using Breeze.Components.Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Breeze.Components.DAL.MongoDB.Adapter
{
    public class MongoCoreComponentsAdapter : ICoreComponentsAdapter
    {
        private readonly IMongoDatabase db;
        private readonly IMongoComponentConverter mongoConverter;

        public MongoCoreComponentsAdapter(IMongoComponentConverter mongoComponentConverter)
        {
            mongoConverter = mongoComponentConverter;

            var client = new MongoClient("mongodb://localhost");
            db = client.GetDatabase("Core");
        }

        private IMongoCollection<MongoUserCoreComponent> Components
            => db.GetCollection<MongoUserCoreComponent>("UserComponents");

        public IEnumerable<ICoreComponent> Get(string userId)
        {
            var components = Components.Find(new BsonDocument("_id", userId)).FirstOrDefault();
            if (components == null)
            {
                return new List<ICoreComponent>();
            }
            var coreComponents = mongoConverter.Convert(components.Components);
            return coreComponents;
        }

        public void Update(string userId, ICoreComponent coreComponent)
        {
            var mongoComponent = mongoConverter.Convert(coreComponent);
            var entry = Components.Find(e => e.UserId == userId).FirstOrDefault();

            if (entry == null)
            {
                Components.InsertOne(new MongoUserCoreComponent {UserId = userId, Components = {mongoComponent}});
            }
            else
            {
                var index = entry.Components.FindIndex(c => c.BreezeId == coreComponent.BreezeId);
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
    }
}