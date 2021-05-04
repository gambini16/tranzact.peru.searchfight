using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using tranzact.peru.businesslayer.SearchEngine;
using tranzact.peru.CommonLayer.SearchExceptions;
using tranzact.peru.entitylayer.Models;

namespace tranzact.peru.searchfight
{
    class Program
    {

        private static async Task Main(string[] args)
        {
            SearchEngine objSearchEngine = new SearchEngine();
            try
            {
                if (args.Length == 0)
                {
                    Console.WriteLine("Enter your query: ");
                    args = Console.ReadLine()?.Split(' ');
                }

                Console.WriteLine("Loading...");

                var resultData = await objSearchEngine.GetSearchGeneralEngine(args?.ToList());

                Console.Clear();
                Console.WriteLine(resultData);
            }
            catch (SearchFightEngineException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error to process data : {ex.Message}");
            }
            Console.ReadKey();


        }
    }
}
