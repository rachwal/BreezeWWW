// // Breeze Project
// // Created by Bartosz Rachwal. 
// // Copyright (c) 2016 Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Breeze.Components.DAL.MongoDB.Model
{
    public class MongoCoreComponent
    {
        [BsonId]
        public string BreezeId { get; set; }

        public string Name { get; set; }
        public List<MongoPort> InPorts { get; set; } = new List<MongoPort>();
        public List<MongoPort> OutPorts { get; set; } = new List<MongoPort>();
    }

    public class MongoUserCoreComponent
    {
        [BsonId]
        public string UserId { get; set; }

        public List<MongoCoreComponent> Components { get; set; } = new List<MongoCoreComponent>();
    }
}