using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreDatabaseFirstSample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace EFCoreDatabaseFirstSample.Controllers
{
    [Route("api/mongo/")]
    [ApiController]
    public class MongoDBController : ControllerBase
    {
        static readonly string connectionString = "mongodb://localhost:27017";
        static MongoClient client = new MongoClient(connectionString);
        IMongoDatabase db = client.GetDatabase("DatabaseForNow");
        

        public MongoDBController() {
        }

        public async Task CreateIndex()
        {
            var collection = db.GetCollection<Book>("Book");
            var indexOptions = new CreateIndexOptions();
            var indexKeys = Builders<Book>.IndexKeys.Ascending(book => book.Title);
            var indexModel = new CreateIndexModel<Book>(indexKeys, indexOptions);
            await collection.Indexes.CreateOneAsync(indexModel);
        }

        [Route("add")]
        [HttpPost]
        public async Task AddBook(Book book)
        {
            var collection = db.GetCollection<BsonDocument>("Book");
            var document = book.ToBsonDocument();
            await collection.InsertOneAsync(document);
        }

        [Route("addAll")]
        [HttpPost]
        public async Task AddAll(List<Book> list)
        {
            var collection = db.GetCollection<BsonDocument>("Book");
            var newList = new List<BsonDocument>();
            list.ForEach(item =>  newList.Add(item.ToBsonDocument()));
            await collection.InsertManyAsync(newList);
        }

        [Route("update")]
        [HttpPut]
        public  OkResult UpdateBook(Book book)
        {
            var collection = db.GetCollection<BsonDocument>("Book");
            var document = book.ToBsonDocument();
            BsonDocument filter = new BsonDocument{ { "_id", book.Id } };
            ReplaceOneResult x = collection.ReplaceOne(filter,  document);
            return Ok();
        }

        [Route("remove")]
        [HttpDelete]
        public  OkResult DeleteBook(int id)
        {
            var collection = db.GetCollection<BsonDocument>("Book");
            BsonDocument filter = new BsonDocument { { "_id", id} };
            collection.DeleteOne(filter);
            return Ok();
        }

        [Route("getById")]
        [HttpGet]
        public Book Get(int id)
        {
            var collection = db.GetCollection<BsonDocument>("Book");
            var bookBson = collection.Find(new BsonDocument { { "_id", id } }).First();
            var book = BsonSerializer.Deserialize<Book>(bookBson);
            return book;
        }

        [Route("getAll")]
        [HttpGet]
        public  List<Book> Get()
        {
            var collection = db.GetCollection<BsonDocument>("Book");
            var list = new List<Book>();
            collection.Find(new BsonDocument()).ToList().ForEach(doc => { list.Add(BsonSerializer.Deserialize<Book>(doc)); });
            return list;
        }
    }
}