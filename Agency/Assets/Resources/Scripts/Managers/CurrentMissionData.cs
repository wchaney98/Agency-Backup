using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class CurrentMissionData
{
    public static int EnemiesKilled;
    public static float TimeTaken;
    public static int MoneyEarned;
    public static int ReputationEarned;

    public static void Reset()
    {
        EnemiesKilled = 0;
        TimeTaken = 0;
        MoneyEarned = 0;
        ReputationEarned = 0;
    }

    public static String GetString()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("<size=56>Mission Complete</size>");
        sb.AppendLine("<size=30>Total Time: " + TimeTaken + "</size>");
        sb.AppendLine("<size=30>Total Enemies Killed: " + EnemiesKilled + "</size>");
        sb.AppendLine("<size=30>Total Money Earned: " + MoneyEarned + "</size>");
        sb.AppendLine("<size=30>Total Reputation Earned: " + ReputationEarned + "</size>");

        return sb.ToString();
    }
}
