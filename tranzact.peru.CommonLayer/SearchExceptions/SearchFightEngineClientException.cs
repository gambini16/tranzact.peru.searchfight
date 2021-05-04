using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tranzact.peru.CommonLayer.SearchExceptions
{
    public class SearchFightEngineClientException : SearchFightEngineException
    {
        public SearchFightEngineClientException(string message) : base(message) { }
        public SearchFightEngineClientException(string message, Exception innerException) : base(message, innerException) { }
    }
}
