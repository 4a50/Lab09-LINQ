using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Linqs
{
    class Program
    {
        static void Main(string[] args)
        {

            string path = "../../../../jsonFiles/data.json";
            string readFile = File.ReadAllText(path);
            List<Feature> manhattanPlaces = new List<Feature>();
            //Using JSON Convert reads file and create an Example object
            FeatureCollection places = JsonConvert.DeserializeObject<FeatureCollection>(readFile);

            ListAll(places);
            Neighborhoods(places);
            NoDupes(places);
            SingleQuery(places);
            //Example of using LINQ method
            var listAllPlaces = places.features.Select(feature => feature.properties.neighborhood);
            int counter = 1;
            Console.WriteLine("Method LINQ\n");
            foreach (var f in listAllPlaces)
            {
                Console.WriteLine($"{counter} {f}");
                counter++;
            }
        }
        public static void ListAll(FeatureCollection ex)
        {
            var filter = (from Feature in ex.features
                          select Feature.properties.neighborhood);
        }

        /// <summary>
        /// Filters out any neighborhoods that are blank.
        /// </summary>
        /// <param name="ex"></param>
        public static void Neighborhoods(FeatureCollection ex)
        {
            var filter = from Feature in ex.features
                         where Feature.properties.neighborhood != ""
                         select Feature.properties.neighborhood;

            int counter = 0;
            foreach (var feature in filter)
            {
                counter++;
                Console.WriteLine($"{counter} {feature}");
            }
        }
        /// <summary>
        /// In addition to filtering out empty neighborhood, it won't return any duplicates.       
        /// </summary>
        /// <param name="ex"></param>
        public static void NoDupes(FeatureCollection ex)
        {
            var noDupes = (from Feature in ex.features
                           where (Feature.properties.neighborhood != "")
                           select (Feature.properties.neighborhood)).Distinct();
            int counter = 0;
            Console.WriteLine("Dupes");
            foreach (var feature in noDupes)
            {
                counter++;
                Console.WriteLine($"{counter} {feature.ToString()}");
            }
        }
        /// <summary>
        /// This method displays the consolidation of LINQ to a single query 
        /// a single query per requirments
        /// 
        /// </summary>
        /// <param name="ex"></param>
        public static void SingleQuery(FeatureCollection ex)
        {
            var noDupes = (from Feature in ex.features
                           where (Feature.properties.neighborhood != "")
                           select (Feature.properties.neighborhood)).Distinct();
            int counter = 0;
            Console.WriteLine("Single Query Line");
            foreach (var feature in noDupes)
            {
                counter++;
                Console.WriteLine($"{counter} {feature.ToString()}");
            }
        }
    }
}
