using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ranking.Domain.Enum
{
    public enum MatchRound
    {
        [Description("Play Off")]
        Playoff = 1,
        [Description("Group")]
        Group = 2,
        [Description("Round of 16")]
        Round16 = 3,
        [Description("Quarterfinal")]
        Quarterfinal = 4,
        [Description("Semifinal")]
        Semifinal = 5,
        [Description("Third Place")]
        ThirdPlace = 6,
        [Description("Final")]
        Final = 7
    }
}
