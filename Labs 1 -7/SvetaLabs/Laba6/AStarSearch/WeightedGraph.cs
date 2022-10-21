using System.Collections.Generic;

namespace SvetaLabs.AStarSearch.Laba6
{
    // Для A* нужен только WeightedGraph и тип точек L, и карта *не*
    // обязана быть сеткой. Однако в коде примера я использую сетку.

    // Ось інтерфейс графу, що представляє сітку із зваженими ребрами
    public interface WeightedGraph<L>
    {
        double Cost(Location a, Location b);
        IEnumerable<Location> Neighbors(Location id);
    }
}
