﻿using dotnet_express_mapper.Models;
using ExpressMapper;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace dotnet_express_mapper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

    }
}
