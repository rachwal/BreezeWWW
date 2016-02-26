// // Breeze Project
// // Created by Bartosz Rachwal. 
// // Copyright (c) 2016 Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using Breeze.SystemBuilder.Adapter;
using Breeze.SystemBuilder.DAL.MongoDB.Model;
using Breeze.SystemBuilder.Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Breeze.SystemBuilder.DAL.MongoDB.Adapter
{
    public class MongoSystemBuilderAdapter : ISystemBuilderAdapter
    {
        private readonly IMongoDatabase db;

        public MongoSystemBuilderAdapter()
        {
            var client = new MongoClient("mongodb://localhost");
            db = client.GetDatabase("SystemBuilder");
        }

        private IMongoCollection<MongoSystemBuilderLayout> Layout
            => db.GetCollection<MongoSystemBuilderLayout>("Layout");

        public void Create(string id, string content)
        {
            Layout.InsertOne(new MongoSystemBuilderLayout {UserId = id, Content = content});
        }

        public ISystemBuilderLayout Retrive(string userId)
        {
            return Layout.Find(new BsonDocument("_id", userId)).FirstOrDefault();
        }

        public void Update(string id, string content)
        {
            var filter = Builders<MongoSystemBuilderLayout>.Filter.Eq(s => s.UserId, id);
            Layout.ReplaceOne(filter, new MongoSystemBuilderLayout {UserId = id, Content = content},
                new UpdateOptions {IsUpsert = true});
        }
    }
}