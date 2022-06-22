namespace Models;

public class Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Point Move(int velocityX, int velocityY) {
        X += velocityX;
        Y += velocityY;
        return this;
    }

    public override string ToString()
    {
        return $"x: {X}; y: {Y}";
    }
}