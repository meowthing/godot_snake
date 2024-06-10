using Godot;
using Vector2 = Godot.Vector2;

[Tool]
public partial class Grid : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Grid2");
	}

	public override void _Draw()
	{
		base._Draw();
		DrawGrid();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	
	public void DrawGrid()
	{
		// Vector2 gridSize = new Vector2(100, 100);
		// Vector2 cellSize = new Vector2(10, 10);

		// Vector2 cellsAmount = new Vector2(gridSize.X / cellSize.X, gridSize.Y / cellSize.Y);		

		int rows = 10;
		int columns = 15;
		int squareSizePx = 50;
		int gridGap = 2;

		for (int x = 0; x < columns; x++)
		{
			for (int y = 0; y < rows; y++)
			{
				DrawRect(new Rect2(new Vector2((squareSizePx + gridGap) * x, (squareSizePx + gridGap) * y), squareSizePx, squareSizePx), Colors.White);
			}
		}
	}

	public void DrawConnect4()
	{
		int COLUMNS = 7;
		int ROWS = 6;
		DrawRect(new Rect2(0, 0, COLUMNS * 100 + 20, ROWS * 100 + 20), new Color("#0000FF"));
		for (int x = 0; x < ROWS; x++)
		{
			for (int y = 0; y < COLUMNS; y++)
			{
				DrawCircle(new Vector2(60 + x * 100, 60 + y * 100), 40, new Color("#ffffff"));
			}
		}
	}
}
