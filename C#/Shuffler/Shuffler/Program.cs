using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Shuffler
{
    class Shuffler
    {
        //fields
        private List<object> listObjects = new List<object>();

        public void FisherYates(List<object> ListObjects)
        {
            this.listObjects = ListObjects;

            //for randomness
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();

            //shuffle values using Fisher-Yates
            int n = listObjects.Count;

            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box); while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;

                object value = listObjects[k];
                listObjects[k] = listObjects[n];
                listObjects[n] = value;
            }
        }

        public void Shuffle()
        {
            Console.WriteLine("Please enter a list of objects" +
                              " to be shuffled...");

            var values = Console.ReadLine();
            string[] inputStrings = values.Split(' ');

            //adding input to list
            foreach (var value in inputStrings)
            {
                listObjects.Add(value);
            }

            //shuffle
            FisherYates(listObjects);

            //print to console
            foreach (var value in listObjects)
            {
                Console.WriteLine(value);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //gogogo
            Shuffler sh = new Shuffler();
            sh.Shuffle();
        }
    }
}
