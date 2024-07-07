using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ranking.API.DTO
{
    public class PlayerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public int Position { get; set; }
        public int TeamID { get; set; }
        public int Dorsal { get; set; }
        public int ConfederationsGoals { get; set; }
        public int WorldCupGoals { get; set; }
        public int ConfederationsGoldenBoots { get; set; }
        public int WorldCupGoldenBoots { get; set; }
    }
}
