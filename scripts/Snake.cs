using Vector2 = Godot.Vector2;
using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class Snake : Node2D
{
    public Minisnake head = new Minisnake();
    public List<Minisnake> minisnakes = new List<Minisnake>();
    private Vector2 curr_direction = Vector2.Right;
    private Vector2 next_direction = Vector2.Right;
    private Tween tween_move;
    private Game game;

    [Signal]
    public delegate void SnakeHitEventHandler(Minisnake minisnakeHit);

    public override void _Ready()
    {
        game = (Game)GetNode("/root/Game");
        head.size = Grid.CellSize;
        head.color = Colors.Red;

        minisnakes.Add(head);

        // repeatedly call Move with a delay to control how fast
        // the snake moves
        tween_move = CreateTween().SetLoops();
        tween_move.TweenCallback(Callable.From(Move)).SetDelay(0.15);

        // Bind the signal handler
        SnakeHit += Hit;
    }

    public override void _Process(double delta)
    {
        QueueRedraw();
    }

    public override void _Draw()
    {
        foreach (Minisnake msnake in minisnakes)
        {
            DrawRect(msnake.GetRectangle(), msnake.color);
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("move_left") && curr_direction != Vector2.Right)
        {
            next_direction = Vector2.Left;
        }
        if (@event.IsActionPressed("move_right") && curr_direction != Vector2.Left)
        {
            next_direction = Vector2.Right;
        }
        if (@event.IsActionPressed("move_up") && curr_direction != Vector2.Down)
        {
            next_direction = Vector2.Up;
        }
        if (@event.IsActionPressed("move_down") && curr_direction != Vector2.Up)
        {
            next_direction = Vector2.Down;
        }

        // test
        if (@event.IsActionPressed("grow"))
        {
            Grow();
        }
    }

    private void Move()
    {
        curr_direction = next_direction;
        Vector2 next_position = head.curr_position + (curr_direction * Grid.CellSize);

        // Wrap the position of the snake head around the screen 
        // using positive modulo trick
        next_position.X = Utils.Repeat(next_position.X, Grid.GridSize.X);
        next_position.Y = Utils.Repeat(next_position.Y, Grid.GridSize.Y);

        head.curr_position = next_position;

        // process the rest of the snake segments
        for (int i = 1; i < minisnakes.Count; i++)
        {
            minisnakes[i].curr_position = minisnakes[i - 1].prev_position;
        }

        // check for collision between head and snake segments
        for (int i = 1; i < minisnakes.Count; i++)
        {
            if (head.GetRectangle().Intersects(minisnakes[i].GetRectangle()))
            {
                EmitSignal(SignalName.SnakeHit, minisnakes[i]);
                game.EmitSignal(Game.SignalName.GameOver);
                break;
            }
        }
    }

    public void Grow()
    {
        Minisnake ms = new Minisnake();
        Minisnake last_minisnake = minisnakes.Last();

        ms.curr_position = last_minisnake.curr_position;
        ms.color = Colors.Pink;
        ms.size = Grid.CellSize;

        minisnakes.Add(ms);

        game.Score += 1;
    }

    private async void Hit(Minisnake minisnakeHit)
    {
        // stop the snake from moving
        tween_move.Kill();

        // avoid segment position changes affecting other systems 
        // (i.e. showing which segment the head hit)
        // by waiting for the next frame to get processed
        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);

        // move snake segments back after confirming intersection
        // to show head position just before disaster strikes
        foreach (Minisnake ms in minisnakes)
        {
            ms.GoToPrevPosition();
        }
    }
}
