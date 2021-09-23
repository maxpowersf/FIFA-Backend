using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking.Domain.Response
{
    public class Group
    {
        public string GroupName { get; set; }
        public List<GroupPosition> Positions { get; set; }

        public Group()
        {
            Positions = new List<GroupPosition>();
        }
    }
}
