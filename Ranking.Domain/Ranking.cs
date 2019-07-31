using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking.Domain
{
    public class Ranking
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int TeamID { get; set; }
        public decimal Points { get; set; }
    }
}
