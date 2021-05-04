using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tranzact.peru.CommonLayer.SearchExceptions
{
    public class SearchFightEngineException:Exception
    {
        public SearchFightEngineException(string message) : base(message) { }
        public SearchFightEngineException(string message, Exception innerException) : base(message, innerException) { }
    }
}
