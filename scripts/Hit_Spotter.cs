using Godot;
using Vector2 = Godot.Vector2;

public partial class Hit_Spotter : Node2D
{
	private Snake snake;

	public Rect2 hit_spot = new Rect2(Vector2.Zero, Grid.CellSize);
	public Color hit_color = Colors.Transparent;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		snake = (Snake)GetNode("%Snake");

		//Bind the signal handler
		snake.SnakeHit += OnSnakeHit;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		QueueRedraw();
	}

	public override void _Draw()
	{
		DrawRect(hit_spot, hit_color);
	}

	public void OnSnakeHit(Minisnake ms)
	{
		hit_spot.Position = ms.curr_position;

		// animate the hit segment to pulse
		var tween_pulse = CreateTween().SetTrans(Tween.TransitionType.Circ).SetLoops();
		tween_pulse.TweenProperty(this, "hit_color", Colors.Purple, .55).SetEase(Tween.EaseType.Out);
		tween_pulse.TweenProperty(this, "hit_color", Colors.Transparent, .55).SetEase(Tween.EaseType.In).SetDelay(.1);
	}
}
