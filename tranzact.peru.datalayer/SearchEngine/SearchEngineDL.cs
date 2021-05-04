using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using tranzact.peru.CommonLayer.Extension;
using tranzact.peru.CommonLayer.SearchExceptions;
using tranzact.peru.CommonLayer.Utility;
using tranzact.peru.entitylayer.Models;
using tranzact.peru.entitylayer.Models.Bing;
using tranzact.peru.entitylayer.Models.Google;


namespace tranzact.peru.datalayer.SearchEngine
{
    public class SearchEngineDL
    {
        public async Task<long> GetResultsAsync(string stringSearch, string clientName)
        {
            long totalResults;

            switch (clientName)
            {
                case ("MSN Search"):
                    totalResults = await GetResultsCountBingAsync(stringSearch);
                    break;
                case ("Google"):
                    totalResults = await GetResultsCountGoogleAsync(stringSearch);
                    break;
                default:
                    totalResults = 0;
                    break;

            }

            return totalResults;

        }

        public async Task<long> GetResultsCountBingAsync(string stringSearch)
        {
            HttpClient _httpClient = new HttpClient();

            if (string.IsNullOrWhiteSpace(stringSearch))
                throw new ArgumentNullException(nameof(stringSearch));

            try
            {
                _httpClient.BaseAddress = new Uri(ConfigKeys.BingSearchUri);
                _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", $"{ConfigKeys.BingSearchKey}");

                using (var response = await _httpClient.GetAsync($"?q={stringSearch}"))
                {
                    if (!response.IsSuccessStatusCode)
                        throw new SearchFightEngineHttpException(
                            "There was an error processing data");

                    var result = await response.Content.ReadAsStringAsync();
                    var bingResponse = result.DeserializeJson<BingResponseEL>();

                    return bingResponse.WebPages.TotalEstimatedMatches;
                }
            }
            catch (Exception ex)
            {
                throw new SearchFightEngineHttpException(ex.Message);
            }
        }

        public async Task<long> GetResultsCountGoogleAsync(string stringSearch)
        {
            HttpClient _httpClient = new HttpClient();

            if (string.IsNullOrWhiteSpace(stringSearch))
                throw new ArgumentNullException(nameof(stringSearch));

            try
            {

                string googleUrl = ConfigKeys.GoogleSearchUri
                    .Replace("{0}", ConfigKeys.GoogleSearchKey)
                    .Replace("{1}", ConfigKeys.GoogleSearchCEKey);

                using (var response = await _httpClient.GetAsync(googleUrl.Replace("{2}", stringSearch)))
                {
                    if (!response.IsSuccessStatusCode)
                        throw new SearchFightEngineHttpException(
                            "There was an error processing data");

                    var result = await response.Content.ReadAsStringAsync();
                    var googleResponse = result.DeserializeJson<GoogleResponseEL>();
                    return long.Parse(googleResponse.SearchInformation.TotalResults);
                }
            }
            catch (Exception ex)
            {
                throw new SearchFightEngineHttpException(ex.Message);
            }
        }
    }
}
