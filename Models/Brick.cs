namespace Models;

public class Brick
{
    public int Id;
    public int HealthPoints = 5;
    public int Width = 43;
    public int Height = 20;
    public bool IsHit = false;
    public Point Point {get; set;}
    public static string[] Color { get; set; } = new string[] {
        "",
        "#a8c418",
        "#c4b618",
        "#c48b18",
        "#c46018",
        "#c41818",
    };

    public Brick(int x, int y, int id, int health = 3)
    {
        Id = id;
        Point = new Point(x, y);
        HealthPoints = health;
    }

    public string BrickStyle() {
        return @$"
        background-color: {Color[HealthPoints]};
        width: {Width}px;
        height: {Height}px;
        left: {Point.X}px;
        top: {Point.Y}px;
        ";
    }
}