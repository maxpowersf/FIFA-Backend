using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ranking.API.DTO
{
    public class ConfederationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal Weight { get; set; }
    }
}
