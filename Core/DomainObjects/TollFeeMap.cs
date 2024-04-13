namespace Core.DomainObjects;

public class TollFeeMap()
{
    public TimeSpan TimeFrom { get; set; }
    public TimeSpan TimeTo { get; set; }
    public int Price { get; set; }

    public bool TimeIsWithinBounds(TimeSpan time)
    {
        return time >= TimeFrom && time <= TimeTo;
    }
}