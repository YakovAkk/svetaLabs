namespace SvetaLabs.AStarSearchAsync.Laba6
{
    public struct LocationAsync
    {
        // Примітки щодо реалізації: я використовую Equals за замовчуванням,
        // Але це може бути повільно. 

        public readonly int x, y;
        public LocationAsync(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
