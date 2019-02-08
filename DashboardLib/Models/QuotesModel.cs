using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace DashboardLib.Models
{
    public class QuotesModel
    {
        
    }
    public class Quotes
    {
        public class Rootobject
        {
            public Success success { get; set; }
            public Contents contents { get; set; }
        }

        public class Success
        {
            public int total { get; set; }
        }

        public class Contents
        {
            public Quote[] quotes { get; set; }
        }

        public class Quote
        {
            public string quote { get; set; }
            public string length { get; set; }
            public string author { get; set; }
            public string[] tags { get; set; }
            public string category { get; set; }
            public string date { get; set; }
            public string title { get; set; }
            public string background { get; set; }
            public string id { get; set; }
        }
    }

}
