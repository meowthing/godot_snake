using System;
using Godot;

public partial class Game : Node
{
	private int _score = 0;
	public int Score
	{
		get
		{
			return _score;
		}
		set
		{
			_score = value;
			EmitSignal(SignalName.ScoreChanged, value);
		}
	}

	[Signal]
	public delegate void ScoreChangedEventHandler(int score);
	[Signal]
	public delegate void GameStartEventHandler();
	[Signal]
	public delegate void GameOverEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
		await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
		EmitSignal(SignalName.GameStart);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Restart()
	{
		Score = 0;
		GetTree().ReloadCurrentScene();
	}
}
