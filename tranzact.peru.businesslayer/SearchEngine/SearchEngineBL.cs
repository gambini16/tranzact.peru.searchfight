using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tranzact.peru.datalayer.SearchEngine;

namespace tranzact.peru.businesslayer.SearchEngine
{
    public class SearchEngineBL
    {
        SearchEngineDL objSearchEngineDL = new SearchEngineDL();

        public async Task<long> GetResultsAsync(string stringSearch, string clientName)
        {
            return await objSearchEngineDL.GetResultsAsync(stringSearch, clientName);
        }

    }
}
