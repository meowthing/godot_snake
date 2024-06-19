using Godot;
using System;

public partial class ctrl_gamestart : Control
{
	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
		Show();
		GetTree().Paused = true;

		while (!Input.IsActionPressed("start"))
		{
			await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
		}

		GetTree().Paused = false;
		CreateTween().TweenProperty(this, "modulate:a", 0, 0.2);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
