using System.Collections.Generic;
using System.Threading.Tasks;

namespace SvetaLabs.AStarSearchAsync.Laba6
{
    // Для A* нужен только WeightedGraph и тип точек L, и карта *не*
    // обязана быть сеткой. Однако в коде примера я использую сетку.
    public interface WeightedGraphAsync<L>
    {
        Task<double> Cost(LocationAsync a, LocationAsync b);
        IEnumerable<LocationAsync> Neighbors(LocationAsync id);
    }
}
