using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreDatabaseFirstSample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EFCoreDatabaseFirstSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MongoDBController : ControllerBase
    {
        static string connectionString = "mongodb://localhost:27017";
        static MongoClient client = new MongoClient(connectionString);
        IMongoDatabase db = client.GetDatabase("DatabaseForNow");

        public MongoDBController() {
        }

        public async Task AddBook(Book book)
        {
            var collection = db.GetCollection<BsonDocument>("Book");
            var document = book.ToBsonDocument();
            await collection.InsertOneAsync(document);
        }

        public async Task UpdateBook(Book book)
        {
            var collection = db.GetCollection<BsonDocument>("Book");
            var document = book.ToBsonDocument();
            var filter = new BsonDocument("id", book.Id.ToString());
            UpdateResult x =  await collection.UpdateOneAsync(filter,  document);
        }

        public async Task DeleteBook(Book book)
        {
            var collection = db.GetCollection<BsonDocument>("Book");
            var document = book.ToBsonDocument();
            await collection.DeleteOneAsync(document);
        }

        public async Task<IEnumerable<BsonDocument>> Get(BsonDocument filter)
        {
            var collection = db.GetCollection<BsonDocument>("Book");
            var list = new List<BsonDocument>();
            await collection.Find(filter).ForEachAsync(document => list.Add(document));
            return list;
        }

        public async Task<IEnumerable<BsonDocument>> Get()
        {
            var collection = db.GetCollection<BsonDocument>("Book");
            var list = new List<BsonDocument>();
            await collection.Find(new BsonDocument()).ForEachAsync(document => { list.Add(document); Console.Write(document); });
            return list;
        }
    }
}