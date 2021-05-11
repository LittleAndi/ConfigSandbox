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

            IConfiguration labelsConfig = new ConfigurationBuilder()
                .AddJsonFile(config["labelconfig"], true)
                .Build();

            System.Console.WriteLine(config["string"]);
            System.Console.WriteLine(config["stringfromappsettings"]);
            System.Console.WriteLine(config["labelconfig"]);
            
            List<Stuff> l = new List<Stuff>();
            config.GetSection("array").Bind(l);
            l.ForEach(a => System.Console.WriteLine(a.Name));

            var cliArray = new List<Stuff>();
            config.GetSection("cliarray").Bind(cliArray);
            cliArray.ForEach(a => System.Console.WriteLine(a.Name));

            System.Console.WriteLine("Label images:");
            var labelImages = new List<LabelImage>();
            labelsConfig.GetSection("labelImages").Bind(labelImages);
            labelImages.ForEach(i => System.Console.WriteLine($"{i.Filename} - ({i.PositionX},{i.PositionY})"));
        }
    }

    internal class Stuff
    {
        public string Name { get; set; }
    }

    internal class LabelImage
    {
        public string Filename { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
    }
}
