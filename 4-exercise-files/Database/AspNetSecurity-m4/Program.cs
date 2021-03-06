﻿using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace AspNetSecurity_m4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "ConfArch Web";

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://localhost:51705")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
