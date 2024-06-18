using Godot;
using System;

public partial class ctrl_gameover : Control
{
    private Game game;
    public override void _Ready()
    {
        Hide();
        SetIndexed("modulate:a", 0);

        game = (Game)GetNode("/root/Game");

        game.GameOver += OnGameOver;
    }

    public async void OnGameOver()
    {
        Show();
        CreateTween().TweenProperty(this, "modulate:a", 1, 0.2).SetEase(Tween.EaseType.In);

        while (!Input.IsActionPressed("start"))
        {
            await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
        }

        game.Restart();
    }
}
