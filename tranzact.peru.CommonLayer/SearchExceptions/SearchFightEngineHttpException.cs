using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace tranzact.peru.CommonLayer.SearchExceptions
{
    public class SearchFightEngineHttpException : SearchFightEngineException
    {
        public SearchFightEngineHttpException(string message) : base(message) { }
        public SearchFightEngineHttpException(string message, Exception innerException) : base(message, innerException) { }
    }
}
