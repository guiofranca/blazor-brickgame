using System.ComponentModel;
using Models;

namespace Services;

public class GameEngine : INotifyPropertyChanged
{
    public Ball Ball;
    public Bar Bar;
    public List<Brick> Bricks;
    public Board Board;
    public int Score { get; set; } = 0;
    public bool GameWon = false;
    public bool GameLost = false;
    public int Level { get; set; } = 1;
    public bool MouseClicked = false;
    public GameEngine(Ball ball, Bar bar, Board board)
    {
        Ball = ball;
        Bar = bar;
        Board = board;
        Bricks = GenerateBricks();
        
    }
    public event PropertyChangedEventHandler? PropertyChanged;

    public bool IsRunning {get; set;} = false;

    public async void GameLoop() {
        while(IsRunning) {
            if(GameLost || GameWon) {
                IsRunning = false;
                break;
            }
            Ball.Move();
            BrickHit();
            Bricks.RemoveAll(b => b.HealthPoints == 0);
            if(Bricks.Count == 0) GameWon = true;
            if(Ball.IsDead()) GameLost = true;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Ball)));
            await Task.Delay(20);
        }
    }

    public void StartGame() {
        IsRunning = true;
    }

    public void PauseGame() {
        IsRunning = false;
    }

    public void BrickHit() {
        Bricks.ForEach(b => {
            var hitBottom = BrickHitBottom(b);
            var hitTop = BrickHitTop(b);
            var hitLeft = BrickHitLeft(b);
            var hitRight = BrickHitRight(b);
            
            b.IsHit = hitBottom || hitTop || hitLeft || hitRight;
            if(b.IsHit) {
                b.HealthPoints--;
                Score++;
            }

            if(hitTop || hitBottom) Ball.ReflectVelocityY();
            if(hitLeft || hitRight) Ball.ReflectVelocityX();
        });
    }

    private bool BrickHitLeft(Brick b) {
        var hit = false;
        if(Ball.Point.X + Ball.Width >= b.Point.X
            && Ball.Point.X + Ball.Width < b.Point.X + Ball.MaxVelocity
            && Ball.Point.Y + Ball.Height >= b.Point.Y
            && Ball.Point.Y < b.Point.Y + b.Height
            && Ball.VelocityX > 0) {
                hit = true;
            }
        return hit;
    }

    private bool BrickHitTop(Brick b) {
        var hit = false;
        if(Ball.Point.X + Ball.Width >= b.Point.X
            && Ball.Point.X < b.Point.X + b.Width
            && Ball.Point.Y + Ball.Height >= b.Point.Y
            && Ball.Point.Y + Ball.Height < b.Point.Y + Ball.MaxVelocity
            && Ball.VelocityY > 0) {
                hit = true;
            }
        return hit;
    }

    private bool BrickHitBottom(Brick b) {
        var hit = false;
        if(Ball.Point.X + Ball.Width >= b.Point.X
            && Ball.Point.X <= b.Point.X + b.Width
            && Ball.Point.Y <= b.Point.Y + b.Height
            && Ball.Point.Y >= b.Point.Y + b.Height - Ball.MaxVelocity
            && Ball.VelocityY < 0) {
                hit = true;
            }
        return hit;
    }

    private bool BrickHitRight(Brick b) {
        var hit = false;
        if(Ball.Point.Y + Ball.Height >= b.Point.Y
            && Ball.Point.Y <= b.Point.Y + b.Height
            && Ball.Point.X <= b.Point.X + b.Width
            && Ball.Point.X >= b.Point.X + b.Width - Ball.MaxVelocity
            && Ball.VelocityX < 0) {
                hit = true;
            }
        return hit;
    }

    public void RestartGame() {
        if(GameWon) {
            Level++;
            Bricks = GenerateBricks(Level);
        } else {
            Level = 1;
            Bricks = GenerateBricks(Level);
        }
        GameWon = false;
        GameLost = false;
        if(GameLost) Score = 0;
        //GameLoop();
        Ball = new Ball(Board, Bar);
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Ball)));
    }

    private List<Brick> GenerateBricks(int rows = 1) {
        var bricks = new List<Brick>();
        int brickCount = 1;
        int health = 5;
        for(int i = 20; i < 250; i+=43) {
            health = rows > 5 ? 5 : rows;
            for(int j = 20; j <= rows*20; j+=20) {
                if(health > 0) bricks.Add(new Brick(i,j, brickCount, health));
                health--;
                brickCount++;
            }
        }
        return bricks;
    }
}