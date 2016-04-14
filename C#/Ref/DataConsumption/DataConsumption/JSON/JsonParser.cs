using System;
using System.Collections.Generic;
using System.Data;
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
        public string SerializeObject()
        {
            JsonObject = JsonConvert.SerializeObject(_account, Formatting.Indented);

            Console.WriteLine("Serialized JSON object:");
            Console.WriteLine(JsonObject);

            return JsonObject;
        }

        /// <summary>
        /// serialize and output list JSON
        /// </summary>
        public string SerializeList()
        {
            JsonList = JsonConvert.SerializeObject(_videogamesList);

            Console.WriteLine("Serialized JSON list:");
            Console.WriteLine(JsonList);

            return JsonList;
        }

        /// <summary>
        /// serialize and output dictionary JSON
        /// </summary>
        public string SerializeDictionary()
        {
            JsonDictionary = JsonConvert.SerializeObject(_points, Formatting.Indented);

            Console.WriteLine("Serialized JSON dictionary:");
            Console.WriteLine(JsonDictionary);

            return JsonDictionary;
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

            Console.WriteLine("Serialized JSON file written to Temp directory...");
        }

        /// <summary>
        /// Serialize a DataSet and it's descending tables and ids
        /// </summary>
        public string SerializeDataset()
        {
            DataSet dataSet = new DataSet("dataSet");
            dataSet.Namespace = "NetFramework";
            DataTable table = new DataTable();
            DataColumn idColumn = new DataColumn("id", typeof(int));
            idColumn.AutoIncrement = true;

            DataColumn itemColumn = new DataColumn("item");
            table.Columns.Add(idColumn);
            table.Columns.Add(itemColumn);
            dataSet.Tables.Add(table);

            for (int i = 0; i < 2; i++)
            {
                DataRow newRow = table.NewRow();
                newRow["item"] = "item" + i;
                table.Rows.Add(newRow);
            }

            dataSet.AcceptChanges();

            string json = JsonConvert.SerializeObject(dataSet, Formatting.Indented);

            Console.WriteLine("Serialized JSON DataSet:");
            Console.WriteLine(json);

            return json;
        }

        /*
         *  Deserializing Methods
         */

        /// <summary>
        /// Deserialize an object, output JSON key-value pair
        /// </summary>
        /// <param name="jsonObject"></param>
        public void DeserializeObject(string jsonObject)
        {
            Account account = JsonConvert.DeserializeObject<Account>(jsonObject);
            
            Console.WriteLine("Deserialized JSON object:");

            Console.WriteLine(account.Email);
        }

        /// <summary>
        /// Deserialize a list, output JSON values
        /// </summary>
        /// <param name="jsonList"></param>
        public void DeserializeList(string jsonList)
        {
            List<string> videogames = JsonConvert.DeserializeObject<List<string>>(jsonList);

            Console.WriteLine("Deserialized JSON list:");

            Console.WriteLine(string.Join(", ", videogames.ToArray()));
        }

        /// <summary>
        /// Deserialize a dictionary, output JSON values
        /// </summary>
        public void DeserializeDictionary()
        {
            string json = @"{
                'href': '/account/login.aspx',
                'target': '_blank'
            }";

            Dictionary<string, string> htmlAttributes = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            Console.WriteLine("Deserialized JSON dictionary:");

            Console.WriteLine(htmlAttributes["href"]);
            Console.WriteLine(htmlAttributes["target"]);
        }

        /// <summary>
        /// Deserialize an anonymous type, output JSON values
        /// </summary>
        public void DeserializeAnonymousTypes()
        {
            var definition = new {Name = ""};

            string json1 = @"{'Name':'James'}";
            var customer1 = JsonConvert.DeserializeAnonymousType(json1, definition);

            string json2 = @"{'Name':'Mike'}";
            var customer2 = JsonConvert.DeserializeAnonymousType(json2, definition);

            Console.WriteLine("Deserialized JSON Anonymous Type:");
            Console.WriteLine(customer1.Name);
            Console.WriteLine(customer2.Name);
        }

        /// <summary>
        /// Deserialize a DataSet, output JSON values
        /// </summary>
        /// <param name="json"></param>
        public void DeserializeDataSet(string json)
        {
            DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(json);

            DataTable dataTable = dataSet.Tables["Table1"];

            Console.WriteLine("Deserialized JSON DataSet:");
            Console.WriteLine(dataTable.Rows.Count);

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine(row["id"] + " - " + row["item"]);
            }
        }

        /// <summary>
        /// Deserialize JSON from a file
        /// </summary>
        public void DeserializeFromFile()
        {
            // read file into a string, deserialize JSON to a type
            Movie movieOne = JsonConvert.DeserializeObject<Movie>(File.ReadAllText(@"c:\Temp\movie.json"));

            Console.WriteLine("Deserialized JSON from file:");
            Console.WriteLine(movieOne.Name);
            Console.WriteLine(movieOne.Year);

            // deserialize directly from file
            using (StreamReader file = File.OpenText(@"c:\Temp\movie.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                Movie movie = (Movie) serializer.Deserialize(file, typeof (Movie));
                Console.WriteLine(movie);
            }
        }
    }
}
