using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace ConfigSandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true)
                .AddCommandLine(args)
                .Build();

            System.Console.WriteLine(config["string"]);
            System.Console.WriteLine(config["stringfromappsettings"]);
            
            List<Stuff> l = new List<Stuff>();
            config.GetSection("array").Bind(l);
            l.ForEach(a => System.Console.WriteLine(a.Name));

            var cliArray = new List<Stuff>();
            config.GetSection("cliarray").Bind(cliArray);
            cliArray.ForEach(a => System.Console.WriteLine(a.Name));
        }
    }

    internal class Stuff
    {
        public string Name { get; set; }
    }
}
