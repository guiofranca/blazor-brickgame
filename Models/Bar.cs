namespace Models;

public class Bar 
{
    private readonly Board Board;
    
    public Bar(Board board)
    {
        Board = board;
        Point = new Point(board.Width/2 - Width/2, 0);
    }
    public int Width { get; set; } = 130;
    public int Height { get; set; } = 10;
    public Point Point { get; set; }

    public const int MoveSpeed = 40;

    public void MoveLeft(){
        if(Point.X > 0) Point.X -= MoveSpeed;
        if(Point.X < 0) Point.X = 0;
    }
    public void MoveRight(){
        if(Point.X < Board.Width - Width) Point.X += MoveSpeed;
        if(Point.X > Board.Width - Width) Point.X = Board.Width - Width;
    }

    public void MoveWithMouse(double position) {
        var newPosition = position - 24 - Width/2;
        Point.X = (int) newPosition;
        if(newPosition > Board.Width - Width) Point.X = Board.Width - Width;
        if(newPosition < 0) Point.X = 0;
    }

    public string BarStyle() {
        return $@"
        height: {Height}px; 
        width: {Width}px; 
        left: {Point.X}px;";
    }
}