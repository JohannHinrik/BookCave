﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookCave.Data;
using BookCave.Data.EntityModels;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BookCave
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            //SeedData();
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
        public static void SeedData()
        {
            var db = new DataContext();
          
            var initialBooks = new List<Book>()
            {
                new Book { Title = "Harry Potter", About = "best book ever", Rating = 5 },
                new Book { Title = "JR Tolkien", Rating = 5 }
            };
            db.AddRange(initialBooks);
            db.SaveChanges();
        }
    }
}
