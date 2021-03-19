using System;
using Microsoft.Extensions.Configuration;

namespace jsonhelp
{
    class Program
    {
        static IConfigurationRoot _config;

        static void Main(string[] args)
        {
            var _builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(path: "config.json");

            _config = _builder.Build();

            var oxApi = new OxfordApi(id: _config["AppId"], key: _config["AppKey"]);

            Console.WriteLine("Type a word you'd like to define:");
            var word = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine($"Definition of {word}");
            Console.WriteLine();
            
            var def = oxApi.DefineOxford(word);
            Console.WriteLine(def);
        }
    }
}
