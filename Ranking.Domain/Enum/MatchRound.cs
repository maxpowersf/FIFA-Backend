using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ranking.Domain.Enum
{
    public enum MatchRound
    {
        [Description("Group")]
        Group = 1,
        [Description("Play Off")]
        Playoff = 2,
        [Description("Round of 16")]
        Round16 = 3,
        [Description("Quarter Final")]
        Quarterfinal = 4,
        [Description("Semi Final")]
        Semifinal = 5,
        [Description("Third Place")]
        ThirdPlace = 6,
        [Description("Final")]
        Final = 7
    }
}
