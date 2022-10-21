using System.Collections.Generic;
using System.Threading.Tasks;

namespace SvetaLabs.AStarSearchAsync.Laba6
{
    public class SquareGridAsync : WeightedGraphAsync<LocationAsync>
    {
        // Примечания по реализации: для удобства я сделал поля публичными,
        // но в реальном проекте, возможно, стоит следовать стандартному
        // стилю и сделать их скрытыми.

        public static readonly LocationAsync[] DIRS = new[]
            {
            new LocationAsync(1, 0),
            new LocationAsync(0, -1),
            new LocationAsync(-1, 0),
            new LocationAsync(0, 1)
        };

        public int width, height;
        public HashSet<LocationAsync> walls = new HashSet<LocationAsync>();
        public HashSet<LocationAsync> forests = new HashSet<LocationAsync>();

        public SquareGridAsync(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public bool InBounds(LocationAsync id)
        {
            return 0 <= id.x && id.x < width
                && 0 <= id.y && id.y < height;
        }

        public bool Passable(LocationAsync id)
        {
            return !walls.Contains(id);
        }

        public async Task<double> Cost(LocationAsync a, LocationAsync b)
        {
            return forests.Contains(b) ? 5 : 1;
        }

        public IEnumerable<LocationAsync> Neighbors(LocationAsync id)
        {
            foreach (var dir in DIRS)
            {
                LocationAsync next = new LocationAsync(id.x + dir.x, id.y + dir.y);
                if (InBounds(next) && Passable(next))
                {
                    yield return next;
                }
            }
        }
    }
}
