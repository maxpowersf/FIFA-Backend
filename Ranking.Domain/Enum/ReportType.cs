using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking.Domain.Enum
{
    public enum ReportType
    {
        Winning = 1,
        Unbeaten = 2,
        Losing = 3,
        Winningless = 4,
        CleanSheets = 5,
        Scoreless = 6,
        ScoringGoals = 7,
        ConcedingGoals = 8
    }
}
