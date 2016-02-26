// // Breeze Project
// // Created by Bartosz Rachwal. 
// // Copyright (c) 2016 Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using Breeze.SystemBuilder.Model;
using MongoDB.Bson.Serialization.Attributes;

namespace Breeze.SystemBuilder.DAL.MongoDB.Model
{
    public class MongoSystemBuilderLayout : ISystemBuilderLayout
    {
        [BsonId]
        public string UserId { get; set; }

        public string Content { get; set; }
    }
}