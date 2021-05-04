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
                    var args2 = Console.ReadLine();

                    var par = Regex.Matches(args2, @"[\""].+?[\""]|[^ ]+")
                             .Cast<Match>()
                             .Select(m => m.Value)
                             .ToList();

                    foreach (string item in par)
                    {

                        if (item.IndexOf("\t") > 0)
                        {
                            item.Replace("\t", "");
                        }


                    }
                    Console.WriteLine("Loading...");

                    var resultData = await objSearchEngine.GetSearchGeneralEngine(par?.ToList());

                    Console.Clear();
                    Console.WriteLine(resultData);
                }

               
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
