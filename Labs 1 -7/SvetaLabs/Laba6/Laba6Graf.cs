using SvetaLabs.AStarSearch.Laba6;
using SvetaLabs.AStarSearchAsync.Laba6;
using SvetaLabs.MeasureTime;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Laba6Graf
{
    public async void Start()
    {
        var measureTheTime = new MeasureTheTime(); // створюємо екземпляр классу який вимірює час 

        Console.WriteLine($"StartWithoutMultiTreading was ended in " +
            $"{measureTheTime.GiveTimeOfWorking(StartWithoutMultiTreading)}"); // вимірюємо час роботи функції StartWithoutMultiTreading


        // var time = await StartWithMultiTreading();

        var time = measureTheTime.GiveTimeOfWorkingOfTask(StartWithMultiTreading).Result;

        Console.WriteLine($"StartWithMultiTreading was ended in " +
            $"{time}"); // вимірюємо час роботи функції StartWithMultiTreading
    }

    private void DrawGrid(SquareGrid grid, AStarSearch astar)
    {
        // Печать массива cameFrom
        for (var y = 0; y < 10; y++)
        {
            for (var x = 0; x < 10; x++)
            {
                Location id = new Location(x, y); // створюємо локаціі у масиві
                Location ptr = id; 
                if (!astar.cameFrom.TryGetValue(id, out ptr)) 
                {
                    ptr = id;
                }
                if (grid.walls.Contains(id)) { Console.Write("##"); }
                else if (ptr.x == x + 1) { Console.Write("\u2192 "); } // проверяем или вокруг точки нету стен
                else if (ptr.x == x - 1) { Console.Write("\u2190 "); } // проверяем или вокруг точки нету стен
                else if (ptr.y == y + 1) { Console.Write("\u2193 "); } // проверяем или вокруг точки нету стен
                else if (ptr.y == y - 1) { Console.Write("\u2191 "); } // проверяем или вокруг точки нету стен
                else { Console.Write("* "); }
            }
            Console.WriteLine();
        }
    }
    private async Task DrawGridAsync(SquareGridAsync grid, AStarSearchAsync astar)
    {
        // Печать массива cameFrom
        for (var y = 0; y < 10; y++)
        {
            for (var x = 0; x < 10; x++)
            {
                LocationAsync id = new LocationAsync(x, y); // створюємо локаціі у масиві
                LocationAsync ptr = id;
                if (!astar.cameFrom.TryGetValue(id, out ptr))
                {
                    ptr = id;
                }
                if (grid.walls.Contains(id)) { Console.Write("##"); }
                else if (ptr.x == x + 1) { Console.Write("\u2192 "); } // проверяем или вокруг точки нету стен
                else if (ptr.x == x - 1) { Console.Write("\u2190 "); } // проверяем или вокруг точки нету стен
                else if (ptr.y == y + 1) { Console.Write("\u2193 "); } // проверяем или вокруг точки нету стен
                else if (ptr.y == y - 1) { Console.Write("\u2191 "); } // проверяем или вокруг точки нету стен
                else { Console.Write("* "); }
            }
            Console.WriteLine();
        }
    }
    private void StartWithoutMultiTreading()
    {
        // Создание "рисунка 4" из предыдущей статьи
        var grid = new SquareGrid(10, 10);
        for (var x = 1; x < 4; x++)
        {
            for (var y = 7; y < 9; y++)
            {
                grid.walls.Add(new Location(x, y));
            }
        }
        grid.forests = new HashSet<Location>
            {
                new Location(3, 4), new Location(3, 5),
                new Location(4, 1), new Location(4, 2),
                new Location(4, 3), new Location(4, 4),
                new Location(4, 5), new Location(4, 6),
                new Location(4, 7), new Location(4, 8),
                new Location(5, 1), new Location(5, 2),
                new Location(5, 3), new Location(5, 4),
                new Location(5, 5), new Location(5, 6),
                new Location(5, 7), new Location(5, 8),
                new Location(6, 2), new Location(6, 3),
                new Location(6, 4), new Location(6, 5),
                new Location(6, 6), new Location(6, 7),
                new Location(7, 3), new Location(7, 4),
                new Location(7, 5)
            };

        // Выполнение A*
        var astar = new AStarSearch(grid, new Location(1, 4),
                                    new Location(8, 5));

        DrawGrid(grid, astar);
    }
    private async Task StartWithMultiTreading()
    {
        // Создание "рисунка 4" из предыдущей статьи
        var grid = new SquareGridAsync(10, 10);
        for (var x = 1; x < 4; x++)
        {
            for (var y = 7; y < 9; y++)
            {
                grid.walls.Add(new LocationAsync(x, y));
            }
        }
        grid.forests = new HashSet<LocationAsync>
            {
                new LocationAsync(3, 4), new LocationAsync(3, 5),
                new LocationAsync(4, 1), new LocationAsync(4, 2),
                new LocationAsync(4, 3), new LocationAsync(4, 4),
                new LocationAsync(4, 5), new LocationAsync(4, 6),
                new LocationAsync(4, 7), new LocationAsync(4, 8),
                new LocationAsync(5, 1), new LocationAsync(5, 2),
                new LocationAsync(5, 3), new LocationAsync(5, 4),
                new LocationAsync(5, 5), new LocationAsync(5, 6),
                new LocationAsync(5, 7), new LocationAsync(5, 8),
                new LocationAsync(6, 2), new LocationAsync(6, 3),
                new LocationAsync(6, 4), new LocationAsync(6, 5),
                new LocationAsync(6, 6), new LocationAsync(6, 7),
                new LocationAsync(7, 3), new LocationAsync(7, 4),
                new LocationAsync(7, 5)
            };

        // Выполнение A*
        var astar = new AStarSearchAsync(grid, new LocationAsync(1, 4),
                                    new LocationAsync(8, 5));

       await DrawGridAsync(grid, astar);
    }
}
