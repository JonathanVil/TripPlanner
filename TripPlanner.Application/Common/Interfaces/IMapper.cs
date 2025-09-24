namespace TripPlanner.Application.Common.Interfaces;

public interface IMapper<in TFrom, out TTo>
{
    public IEnumerable<TTo> Map(IEnumerable<TFrom> from)
    {
        return from.Select(Map);
    }

    public TTo[] Map(TFrom[] from)
    {
        return from.Select(Map).ToArray();
    }
    
    TTo Map(TFrom from);
}