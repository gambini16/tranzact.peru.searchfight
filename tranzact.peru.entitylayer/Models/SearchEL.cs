using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tranzact.peru.entitylayer.Models
{
    public class SearchEL
    {
        public string ClientName { get; set; }
        public string SearchClient { get; set; }
        public string Query { get; set; }
        public long TotalResults { get; set; }

    }
}
