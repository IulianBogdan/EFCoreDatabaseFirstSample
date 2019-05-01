using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EFCoreDatabaseFirstSample.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EFCoreDatabaseFirstSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MainAsync();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        static async void MainAsync()
        {
            var controller = new Controllers.MongoDBController();
            
             var book = new Book();
             book.Id = 23;
             book.Publisher = new Publisher();
             book.Title = "title2222";
             book.PublisherId = 2;
             book.Category = new BookCategory();
             book.BookAuthors = null;
             await controller.UpdateBook(book);
             

            //uncoment this for get method
            //Console.Write(controller.Get().ToString());
            //Console.WriteLine();


        }
    }
}
