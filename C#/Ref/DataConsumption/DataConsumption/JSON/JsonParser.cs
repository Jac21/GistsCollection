using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace DataConsumption.JSON
{
    // Example account class
    public class Account
    {
        public string Email { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public IList<string> Roles { get; set; } 
    }

    // Example movie class
    public class Movie
    {
        public string Name { get; set; }
        public int Year { get; set; }
    }

    // taking advantage of Json.NET
    public class JsonParser
    {
        // class fields
        private readonly Account _account;
        public string JsonObject;

        private readonly List<string> _videogamesList;
        public string JsonList;

        private readonly Dictionary<string, int> _points;
        public string JsonDictionary;

        private readonly Movie _movie;
        public string JsonMovie;

        /// <summary>
        /// Constructor, initialize objects to be serialized
        /// </summary>
        public JsonParser()
        {
            _account = new Account()
            {
                Email = "jcantu521@gmail.com",
                Active = true,
                CreatedDate = new DateTime(2013, 1, 20, 0, 0, 0, DateTimeKind.Utc),
                Roles = new List<string>
                {
                    "User",
                    "Admin"
                }
            };

            _videogamesList = new List<string>
            {
                "Starcraft",
                "Halo",
                "Legend of Zelda"
            };

            _points = new Dictionary<string, int>
            {
                {"James", 9001},
                {"Jo", 3474},
                {"Jess", 11926}
            };

            _movie = new Movie
            {
                Name = "Bad Boys",
                Year = 1995
            };
        }

        /// <summary>
        /// serialize and output object JSON
        /// </summary>
        public void SerializeObject()
        {
            JsonObject = JsonConvert.SerializeObject(_account, Formatting.Indented);

            Console.WriteLine(JsonObject);
        }

        /// <summary>
        /// serialize and output list JSON
        /// </summary>
        public void SerializeList()
        {
            JsonList = JsonConvert.SerializeObject(_videogamesList);

            Console.WriteLine(JsonList);
        }

        /// <summary>
        /// serialize and output dictionary JSON
        /// </summary>
        public void SerializeDictionary()
        {
            JsonDictionary = JsonConvert.SerializeObject(_points, Formatting.Indented);

            Console.WriteLine(JsonDictionary);
        }

        /// <summary>
        /// serialize movie object to file, output
        /// </summary>
        public void SerializeToFile()
        {
            // serialize JSON to a string and then write string to file
            File.WriteAllText(@"c:\Temp\movie.json", JsonConvert.SerializeObject(_movie));

            // serialize JSON directly to file
            using (StreamWriter file = File.CreateText(@"c:\Temp\movie.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, _movie);
            }
        }
    }
}
