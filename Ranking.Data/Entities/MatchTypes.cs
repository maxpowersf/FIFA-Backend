using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking.Data.Entities
{
    public class MatchTypes
    {
        public int MatchTypeID { get; set; }
        public string Name { get; set; }
        public decimal Weight { get; set; }
    }
}
