using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tranzact.peru.businesslayer.SearchEngine;
using tranzact.peru.entitylayer.Models;

namespace tranzact.peru.searchfight.test.SearchEngineTest
{
    [TestClass]
    public class SearchEngineTest
    {
        SearchEngine objSearchEngine = new SearchEngine();

        [TestMethod]
        public void GetSearchGeneralEngine_IsNotNullResults()
        {
            //Arrange
            var stringSearch = new List<string>() { "java .Net" };
            //Act
            var result = objSearchEngine.GetSearchGeneralEngine(stringSearch);

            //Assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void GetWinners_NetGoogleWinner_ShouldGetWinner()
        {
            //Arrange

            string expect = "Google winner: .net";

            var searchResults = new List<SearchEL>
            {
                new SearchEL{Query = ".net",SearchClient = "Google",TotalResults = 7550000},
                new SearchEL{Query = ".net",SearchClient = "MSN Search",TotalResults = 45000},
                new SearchEL{Query = "java",SearchClient = "Google",TotalResults = 152000},
                new SearchEL{Query = "java",SearchClient = "MSN Search",TotalResults = 56000
                },
            };


            var allWinnners = objSearchEngine.GetSearchWinners(searchResults);

            var winnerString = allWinnners.Select(client => $"{client.ClientName} winner: {client.WinnerQuery}")
          .ToList();
            Assert.AreEqual(expect, winnerString[0]);
        }

        [TestMethod]
        public void GetWinners_NetBingWinner_ShouldGetWinner()
        {
            //Arrange

            string expect = "MSN Search winner: .net";

            var searchResults = new List<SearchEL>
            {
                new SearchEL{Query = ".net",SearchClient = "Google",TotalResults = 7550000},
                new SearchEL{Query = ".net",SearchClient = "MSN Search",TotalResults = 855441000},
                new SearchEL{Query = "java", SearchClient = "Google",TotalResults = 152000},
                new SearchEL{Query = "java",SearchClient = "MSN Search",TotalResults = 56000
                },
            };


            var allWinnners = objSearchEngine.GetSearchWinners(searchResults);

            var winnerString = allWinnners.Select(client => $"{client.ClientName} winner: {client.WinnerQuery}")
          .ToList();
            Assert.AreEqual(expect, winnerString[1]);
        }

        [TestMethod]
        public void GetMainResults_ReturnStringDataOK()
        {
            var searchResults = new List<SearchEL>
            {
                new SearchEL{Query = ".net",SearchClient = "Google",TotalResults = 50000},
                new SearchEL{Query = ".net",SearchClient = "MSN Search",TotalResults = 50000},
                new SearchEL{Query = "java",SearchClient = "Google",TotalResults = 3000},
                new SearchEL{Query = "java",SearchClient = "MSN Search",TotalResults = 5000
                },
            };

            var results = objSearchEngine.GetSearchMainResults(searchResults);


            var clientResultsString = results
             .Select(resultsGroup =>
                 $"{resultsGroup.Key}: {string.Join(" ", resultsGroup.Select(client => $"{client.SearchClient}: {client.TotalResults}"))}")
             .ToList();

            Assert.IsInstanceOfType(clientResultsString[0], typeof(string));
        }

    }
}
