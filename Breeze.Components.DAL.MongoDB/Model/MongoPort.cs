// // Breeze Project
// // Created by Bartosz Rachwal. 
// // Copyright (c) 2016 Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using Breeze.Components.Model;
using MongoDB.Bson.Serialization.Attributes;

namespace Breeze.Components.DAL.MongoDB.Model
{
    public class MongoPort : IPort
    {
        [BsonId]
        public string BreezeId { get; set; }

        public string Name { get; set; }
        public string Value { get; set; }
    }
}