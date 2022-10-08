using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SvetaLabs.AStarSearchAsync.Laba6
{
    public class AStarSearchAsync
    {
        public Dictionary<LocationAsync, LocationAsync> cameFrom = new Dictionary<LocationAsync, LocationAsync>();
        public Dictionary<LocationAsync, double> costSoFar = new Dictionary<LocationAsync, double>();

        // Примітка: узагальнена версія A* абстрагується від Location
        // та Heuristic
        public async Task<double> Heuristic(LocationAsync a, LocationAsync b) // асинхронно рахуємо відстань
        {
            return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
        }

        public AStarSearchAsync(WeightedGraphAsync<LocationAsync> graph, LocationAsync start, LocationAsync goal) // запускаємо алгоритм у конструкторі
        {
            CreateAStarSerchAsync(graph, start, goal);
        }

        private async Task CreateAStarSerchAsync(WeightedGraphAsync<LocationAsync> graph, 
            LocationAsync start, LocationAsync goal)
        {
            var frontier = new PriorityQueueAsync<LocationAsync>();
            frontier.Enqueue(start, 0);

            cameFrom[start] = start;
            costSoFar[start] = 0;

            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue();

                if (current.Equals(goal))
                {
                    break;
                }

                foreach (var next in graph.Neighbors(current))
                {
                    var cost = await graph.Cost(current, next);
                    double newCost = costSoFar[current]
                        + cost;
                    if (!costSoFar.ContainsKey(next)
                        || newCost < costSoFar[next])
                    {
                        costSoFar[next] = newCost;
                        var heuristic = await Heuristic(next, goal);
                        double priority = newCost + heuristic;
                        frontier.Enqueue(next, priority);
                        cameFrom[next] = current;
                    }
                }
            }
        }
    }
}
