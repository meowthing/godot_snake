using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using Godot;
using Vector2 = Godot.Vector2;

public partial class Grid : Node2D
{
	private static Vector2 _gridSize = new Vector2(800, 480);
	public static Vector2 GridSize { get { return _gridSize; } }

	private static Vector2 _cellSize = new Vector2(32, 32);
	public static Vector2 CellSize { get { return _cellSize; } }

	public static Vector2 cellsAmount
	{
		get { return new Vector2(_gridSize.X / CellSize.X, _gridSize.Y / CellSize.Y); }
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Grid2");
	}

	public override void _Draw()
	{
		DrawGrid();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void DrawGrid()
	{
		// Draw background
		DrawRect(new Rect2(0, 0, _gridSize.X, _gridSize.Y), Colors.White);

		for (int x = 0; x < cellsAmount.X; x++)
		{
			var from = new Vector2(x * CellSize.X, 0);
			var to = new Vector2(from.X, _gridSize.Y);
			DrawLine(from, to, Colors.Gray);
		}

		for (int y = 0; y < cellsAmount.Y; y++)
		{
			var from = new Vector2(0, y * CellSize.Y);
			var to = new Vector2(_gridSize.X, from.Y);
			DrawLine(from, to, Colors.Gray);
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
