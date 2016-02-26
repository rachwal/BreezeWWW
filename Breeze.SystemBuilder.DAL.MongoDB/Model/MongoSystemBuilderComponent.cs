// // Breeze Project
// // Created by Bartosz Rachwal. 
// // Copyright (c) 2016 Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System.Collections.Generic;
using Breeze.SystemBuilder.Model;
using MongoDB.Bson.Serialization.Attributes;

namespace Breeze.SystemBuilder.DAL.MongoDB.Model
{
    public class MongoSystemBuilderComponent : ISystemBuilderComponent
    {
        public MongoSystemBuilderComponent(ISystemBuilderComponent component)
        {
            BreezeId = component.BreezeId;
            Name = component.Name;
            Color = component.Color;
        }

        [BsonId]
        public string BreezeId { get; set; }

        public string Name { get; set; }
        public string Color { get; set; }
    }

    public class MongoUserSystemBuilderComponents
    {
        [BsonId]
        public string UserId { get; set; }

        public List<MongoSystemBuilderComponent> Components { get; set; } = new List<MongoSystemBuilderComponent>();
    }
}