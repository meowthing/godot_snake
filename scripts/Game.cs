using Godot;
public partial class Game : Node2D
{
	private static int _score = 0;
	public static int Score
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
	public delegate void ScoreChangedEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


}
