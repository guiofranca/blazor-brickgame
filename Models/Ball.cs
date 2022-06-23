namespace Models;

public class Ball 
{
    private readonly Board _board;
    private readonly Bar _bar;
    public Ball(Board board, Bar bar)
    {
        _board = board;
        _bar = bar;
        Point = new Point(_board.Width/2,2*_board.Height/3);
    }
    public int Width = 10;
    public int Height = 10;
    public Point Point {get; set;}
    public int VelocityX { get; set; } = MinVelocity;
    public int VelocityY { get; set; } = -3;
    public const int MaxVelocity = 5;
    public const int MinVelocity = 1;

    public void Move() {
        Point.Move(VelocityX, VelocityY);
        DetectCollisionWithBoard();
        DetectCollisionWithBar();
    }

    private void DetectCollisionWithBoard() {
        if(Point.X >= (_board.Width - Width)) VelocityX = -Math.Abs(VelocityX);
        if(Point.Y >= (_board.Height - Height)) VelocityY = -Math.Abs(VelocityY);
        if(Point.X <= 0) VelocityX = Math.Abs(VelocityX);
        if(Point.Y <= 0) VelocityY = Math.Abs(VelocityY);
    }

    public bool IsDead()
    {
        if(Point.Y >= (_board.Height - Height)) return true;
        return false;
    }

    private void DetectCollisionWithBar() {
        if(_board.Height - (Point.Y + Height) <= _bar.Height 
        && Point.X + Width >= _bar.Point.X 
        && Point.X <= _bar.Width + _bar.Point.X) {
            VelocityY = -Math.Abs(VelocityY);

            if(Point.X >= _bar.Point.X && Point.X < _bar.Point.X + _bar.Width/3) {
                if(VelocityX >= 0 && VelocityX > MinVelocity) VelocityX--;
                if(VelocityX <= 0 && VelocityX > -MaxVelocity) VelocityX--;
            }

            if(Point.X >= _bar.Point.X + 2*_bar.Width/3 && Point.X < _bar.Point.X + _bar.Width) {
                if(VelocityX >= 0 && VelocityX < MaxVelocity) VelocityX++;
                if(VelocityX <= 0 && VelocityX < -MinVelocity) VelocityX++;
            }



        }
    }

    internal void ReflectVelocityY()
    {
        VelocityY = -VelocityY;
    }

    internal void ReflectVelocityX()
    {
        VelocityX = -VelocityX;
    }

    public string Style() {
        return @$"width: {Width}px;
        height: {Height}px;
        top: {Point.Y}px;
        left: {Point.X}px";
    }
}