using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking.Domain
{
    public class MatchType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Weight { get; set; }
    }
}
