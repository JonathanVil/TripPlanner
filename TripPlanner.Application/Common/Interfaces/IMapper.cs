namespace TripPlanner.Application.Common.Interfaces;

public interface IMapper<in TFrom, out TTo>
{
    TTo Map(TFrom from);
}