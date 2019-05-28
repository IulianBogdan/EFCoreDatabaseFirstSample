using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EFCoreDatabaseFirstSample.Controllers;
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
            Task.WaitAll(MakeIndexAsync());
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        public static async Task MakeIndexAsync()
        {
            await new MongoDBController().CreateIndex();
        }
    }
}

    
