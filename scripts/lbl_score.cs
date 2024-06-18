using Godot;

public partial class lbl_score : Label
{
	private Game game;
	private Tween tween;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetIndexed("modulate:a", 0);
		game = (Game)GetNode("/root/Game");

		game.ScoreChanged += OnScoreChanged;
		game.GameOver += OnGameOver;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _ExitTree()
	{
		// Have to manually dispose of the delegates before resetting the scene, 
		// because the Game object never actually resets due to it living 
		// above the current scene
		game.ScoreChanged -= OnScoreChanged;
		game.GameOver -= OnGameOver;
	}

	public void OnScoreChanged(int score)
	{
		Text = score.ToString();

		if (tween != null && tween.IsRunning())
		{
			tween.Kill();
		}
		tween = CreateTween().SetTrans(Tween.TransitionType.Sine);
		tween.TweenProperty(this, "modulate:a", 1, 0.5).SetEase(Tween.EaseType.Out);
		tween.TweenProperty(this, "modulate:a", 0, 0.5).SetEase(Tween.EaseType.In);
	}

	public void OnGameOver()
	{
		if (tween != null && tween.IsRunning())
		{
			tween.Kill();
		}
		tween = CreateTween().SetTrans(Tween.TransitionType.Sine);
		tween.TweenProperty(this, "modulate:a", 1, 0.5).SetEase(Tween.EaseType.Out);
	}
}
