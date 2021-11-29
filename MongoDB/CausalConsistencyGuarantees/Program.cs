using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CausalConsistencyGuarantees
{
    /// <summary>
    /// mongod --port 27017 --replSet rs0 --dbpath="C:\data\db0"
    /// 
    /// rs.initiate()
    /// 
    /// mongod --port 27027 --replSet rs0 --dbpath="C:\data\db1"
    /// mongod --port 27037 --replSet rs0 --dbpath="C:\data\db2"
    /// 
    /// rs.add( { host: "127.0.0.1:27027", priority: 0, votes: 0 } )
    /// rs.add( { host: "127.0.0.1:27037", priority: 0, votes: 0 } )
    /// 
    /// rs.status()
    /// </summary>
    public class Program
    {
        private const string ConnectionString =
            "mongodb://localhost:27017,localhost:27027,localhost:27037/?replicaSet=rs0";

        private const string DatabaseName = "test-db";
        private const string CollectionName = "test-collection";

        public static async Task Main(string[] args)
        {
            Console.WriteLine($"Hello {nameof(CausalConsistencyGuarantees)}!");

            // connect to Mongo replica set
            var client = new MongoClient(ConnectionString);

            // specify that reads go to secondary server
            var collection = client
                .GetDatabase(DatabaseName)
                .GetCollection<BsonDocument>(CollectionName)
                .WithReadPreference(ReadPreference.Secondary);

            var stopWatch = Stopwatch.StartNew();

            // keep inserting documents and trying to read after write,
            // will eventually throw exception since secondary has not caught up yet
            while (stopWatch.Elapsed.Milliseconds < 5000)
            {
                var newDocument = new BsonDocument();

                await collection.InsertOneAsync(newDocument);

                var foundDocument = await collection
                    .FindAsync(Builders<BsonDocument>
                        .Filter.Eq(x => x["_id"], newDocument["_id"]))
                    .Result
                    .FirstOrDefaultAsync();

                if (foundDocument == null)
                {
                    throw new Exception("Document not found");
                }

                Console.WriteLine("Success!");

                await collection.DeleteOneAsync(Builders<BsonDocument>
                    .Filter.Eq(x => x["_id"], newDocument["_id"]));
            }
        }
    }
}