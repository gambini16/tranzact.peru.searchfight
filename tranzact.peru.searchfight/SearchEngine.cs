using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tranzact.peru.businesslayer.SearchEngine;
using tranzact.peru.CommonLayer.Extension;
using tranzact.peru.CommonLayer.SearchExceptions;
using tranzact.peru.entitylayer.Models;

namespace tranzact.peru.searchfight
{
    public class SearchEngine
    {

        SearchEngineBL objSearchEngineBL = new SearchEngineBL();


        public async Task<string> GetSearchGeneralEngine(List<string> stringSearch)
        {
            StringBuilder _stringBuilder = new StringBuilder();


            if (stringSearch == null)
                throw new ArgumentNullException(nameof(stringSearch));

            try
            {

                var searchEngineResults = await GetSerachResultsAsync(stringSearch.Distinct());

                var allWinnners = GetSearchWinners(searchEngineResults);
                var generalTotalWinner = GetSearchTotalWinner(searchEngineResults);
                var mainResults = GetSearchMainResults(searchEngineResults);


                var clientResultsString = mainResults
                .Select(resultsGroup =>
                    $"{resultsGroup.Key}: {string.Join(" ", resultsGroup.Select(client => $"{client.SearchClient}: {client.TotalResults}"))}")
                .ToList();

                var winnerString = allWinnners.Select(client => $"{client.ClientName} winner: {client.WinnerQuery}")
                 .ToList();

                var totallWinnerString = $"Total winner: {generalTotalWinner}";

                clientResultsString.ForEach(queryResults => _stringBuilder.AppendLine(queryResults));
                winnerString.ForEach(winners => _stringBuilder.AppendLine(winners));

                _stringBuilder.AppendLine(totallWinnerString);

                return _stringBuilder.ToString();

            }
            catch (SearchFightEngineClientException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error to search general engine: {ex.Message}");
            }

            return string.Empty;
        }
        public async Task<List<SearchEL>> GetSerachResultsAsync(IEnumerable<string> stringSearch)
        {

            var listSearchEL = GetListSearchEngines();

            var result = new List<SearchEL>();

            foreach (var item in stringSearch)
            {
                foreach (var searchClient in listSearchEL)
                {
                    result.Add(new SearchEL
                    {
                        SearchClient = searchClient.ClientName,
                        Query = item,
                        TotalResults = await objSearchEngineBL.GetResultsAsync(item, searchClient.ClientName)
                    }); ;

                }

            }

            return result;

        }
        public IEnumerable<WinnerEL> GetSearchWinners(List<SearchEL> searchResults)
        {
            if (searchResults == null)
                throw new ArgumentNullException(nameof(searchResults));

            var winners = searchResults
                .OrderBy(result => result.SearchClient)
                .GroupBy(result => result.SearchClient, result => result,
                    (client, result) => new WinnerEL
                    {
                        ClientName = client,
                        WinnerQuery = result.MaxValue(r => r.TotalResults).Query
                    });

            return winners;
        }
        public string GetSearchTotalWinner(List<SearchEL> searchResults)
        {
            if (searchResults == null)
                throw new ArgumentNullException(nameof(searchResults));

            var totalWinner = searchResults
                .OrderBy(result => result.SearchClient)
                .GroupBy(result => result.Query, result => result,
                    (query, result) => new { Query = query, Total = result.Sum(r => r.TotalResults) })
                .MaxValue(r => r.Total).Query;

            return totalWinner;
        }
        public IEnumerable<IGrouping<string, SearchEL>> GetSearchMainResults(List<SearchEL> searchResults)
        {
            if (searchResults == null)
                throw new ArgumentNullException(nameof(searchResults));

            var results = searchResults
                .OrderBy(result => result.SearchClient)
                .ToLookup(result => result.Query, result => result);

            return results;
        }

        public List<SearchEL> GetListSearchEngines()
        {
            List<SearchEL> listSearchEL = new List<SearchEL>();

            listSearchEL.Add(new SearchEL { ClientName = "MSN Search" });
            listSearchEL.Add(new SearchEL { ClientName = "Google" });

            return listSearchEL;
        }
    }
}
