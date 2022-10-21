namespace SvetaLabs.AStarSearch.Laba6
{
    public struct Location
    {
        // Примітки щодо реалізації: я використовую Equals за замовчуванням,
        // Але це може бути повільно. 

        public readonly int x, y;
        public Location(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
