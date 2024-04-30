namespace Assets.Scripts.Stats
{
    public enum StatsType
    {
        Health,
        HitPoints,
        Speed,
        SpecialPoints
    }


    static class StatsTypeExtensions
    {
        public static string GetDescription(this StatsType statsType)
        {
            switch (statsType)
            {
                case StatsType.Health:
                    return "Health";
                case StatsType.HitPoints:
                    return "Hit Points";
                case StatsType.Speed:
                    return "Speed";
                case StatsType.SpecialPoints:
                    return "Special Points";
                default:
                    return "Unknown";
            }
        }
    }
}