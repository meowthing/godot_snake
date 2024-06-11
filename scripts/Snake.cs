using Vector2 = Godot.Vector2;
using Godot;
using System.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Snake : Node2D
{
    private Minisnake head = new Minisnake();
    private List<Minisnake> minisnakes = new List<Minisnake>();
    private Vector2 curr_direction = Vector2.Right;
    private Vector2 next_direction = Vector2.Right;
    private Tween tween_move;


    public override void _Ready()
    {
        head.size = Grid.CellSize;
        head.color = Colors.Red;

        minisnakes.Add(head);

        tween_move = CreateTween().SetLoops();
        tween_move.TweenCallback(Callable.From(Move)).SetDelay(0.15);
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

        //Testing only
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
    }

    private void Grow()
    {
        Minisnake ms = new Minisnake();
        Minisnake last_minisnake = minisnakes.Last();

        ms.curr_position = last_minisnake.curr_position;
        ms.color = Colors.Pink;
        ms.size = Grid.CellSize;

        minisnakes.Add(ms);
    }
}
